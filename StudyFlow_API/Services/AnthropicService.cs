using Anthropic.SDK.Constants;

namespace StudyFlow.Services;

public class AnthropicService
{
    private readonly AnthropicClient _client;
    private readonly ICourseMaterialRepository _materialRepo;
    private readonly IActivityRepository _activityRepo;
    private readonly IClassSessionRepository _sessionRepo;
    private readonly IProgramScheduleRepository _scheduleRepo;

    public AnthropicService(
        IConfiguration configuration,
        ICourseMaterialRepository materialRepo,
        IActivityRepository activityRepo,
        IClassSessionRepository sessionRepo,
        IProgramScheduleRepository scheduleRepo)
    {
        _client = new AnthropicClient(configuration["ANTHROPIC_API_KEY"]);
        _materialRepo = materialRepo;
        _activityRepo = activityRepo;
        _sessionRepo = sessionRepo;
        _scheduleRepo = scheduleRepo;
    }

    private static string StripMarkdown(string text)
    {
        text = text.Trim();
        if (!text.StartsWith("```")) return text;
        var firstNewline = text.IndexOf('\n');
        var lastBacktick = text.LastIndexOf("```");
        if (firstNewline >= 0 && lastBacktick > firstNewline)
            return text.Substring(firstNewline + 1, lastBacktick - firstNewline - 1).Trim();
        return text;
    }

    public async Task ProcessMaterialAsync(CourseMaterial material)
    {
        var fileBytes = await File.ReadAllBytesAsync(material.BlobRef);
        var base64 = Convert.ToBase64String(fileBytes);

        var messages = new List<Message>
        {
            new Message
            {
                Role = RoleType.User,
                Content = new List<ContentBase>
                {
                    new DocumentContent
                    {
                        Source = new DocumentSource
                        {
                            Type = SourceType.base64,
                            MediaType = "application/pdf",
                            Data = base64
                        }
                    },
                    new TextContent
                    {
                        Text = "Analyze this course material and respond ONLY with a JSON object, no markdown, no explanation:\n" +
                               "{\n" +
                               "  \"topic\": \"main topic in 3-5 words\",\n" +
                               "  \"difficulty\": <integer 1-5>,\n" +
                               "  \"estimatedStudyMinutes\": <integer>\n" +
                               "}"
                    }
                }
            }
        };

        var response = await _client.Messages.GetClaudeMessageAsync(new MessageParameters
        {
            Model = AnthropicModels.Claude45Haiku,
            MaxTokens = 1024,
            Messages = messages
        });

        var json = StripMarkdown(response.Message.ToString());
        var result = JsonSerializer.Deserialize<MaterialMetadata>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        material.ExtractedTopic = result.Topic;
        material.ExtractedDifficulty = result.Difficulty;
        material.EstimatedStudyTime = result.EstimatedStudyMinutes;

        await _materialRepo.UpdateAsync(material);
    }

    public async Task<bool> ProcessAllMaterialsAsync()
    {
        var materials = await _materialRepo.GetAllAsync();
        var supported = materials
            .Where(m => m.BlobRef.EndsWith(".pdf") && m.ExtractedTopic == null)
            .ToList();

        if (!supported.Any()) return false;

        foreach (var material in supported)
            await ProcessMaterialAsync(material);

        return true;
    }

    public async Task<bool> GenerateScheduleAsync()
    {
        var existing = await _scheduleRepo.GetLatestAsync();
        if (existing != null) return false;

        var materials = await _materialRepo.GetAllAsync();
        var activities = await _activityRepo.GetAllAsync();
        var sessions = await _sessionRepo.GetAllAsync();

        var sessionsJson = JsonSerializer.Serialize(sessions.Select(s => new { s.Course!.Name, s.SessionType, s.DayOfWeek, s.StartTime, s.EndTime }));
        var activitiesJson = JsonSerializer.Serialize(activities.Select(a => new { a.Course!.Name, a.Title, a.Type, a.DueDate, a.EstimatedEffort }));
        var materialsJson = JsonSerializer.Serialize(materials.Where(m => m.ExtractedTopic != null).Select(m => new { m.Course!.Name, m.Title, m.ExtractedTopic, m.ExtractedDifficulty, m.EstimatedStudyTime }));

        var prompt = "You are a study schedule generator. Given the following academic data, generate an optimized weekly study schedule.\n" +
                     "Generate a 2-week schedule only. Do not generate more than 2 weeks.\n\n" +
                     "Respond ONLY with a JSON object, no markdown, no explanation.\n\n" +
                     "CLASS SESSIONS (fixed commitments, do not schedule study time here):\n" +
                     sessionsJson + "\n\n" +
                     "ACTIVITIES (deadlines to prepare for):\n" +
                     activitiesJson + "\n\n" +
                     "COURSE MATERIALS (topics to study, with difficulty):\n" +
                     materialsJson + "\n\n" +
                     "Generate a schedule in this format:\n" +
                     "{\n" +
                     "  \"weeks\": [\n" +
                     "    {\n" +
                     "      \"week\": 1,\n" +
                     "      \"days\": [\n" +
                     "        {\n" +
                     "          \"day\": \"Monday\",\n" +
                     "          \"blocks\": [\n" +
                     "            { \"time\": \"10:00-12:00\", \"type\": \"class\", \"course\": \"DevOps\", \"session\": \"Course\" },\n" +
                     "            { \"time\": \"13:00-15:00\", \"type\": \"study\", \"course\": \"DevOps\", \"topic\": \"CI/CD basics\", \"activity\": \"CI/CD Pipeline Setup\" }\n" +
                     "          ]\n" +
                     "        }\n" +
                     "      ]\n" +
                     "    }\n" +
                     "  ]\n" +
                     "}";

        var messages = new List<Message>
        {
            new Message
            {
                Role = RoleType.User,
                Content = new List<ContentBase>
                {
                    new TextContent { Text = prompt }
                }
            }
        };

        var response = await _client.Messages.GetClaudeMessageAsync(new MessageParameters
        {
            Model = AnthropicModels.Claude45Haiku,
            MaxTokens = 8192,
            Messages = messages
        });

        var json = StripMarkdown(response.Message.ToString());

        await _scheduleRepo.AddAsync(new ProgramSchedule
        {
            GeneratedJSON = json,
            GeneratedAt = DateTime.UtcNow
        });

        return true;
    }
}

public class MaterialMetadata
{
    public string Topic { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public int EstimatedStudyMinutes { get; set; }
}