namespace StudyFlow;

public static class StudyFlowSeeder
{
    public static async Task SeedAsync(StudyFlowDbContext context)
    {
        if (context.Courses.Any()) return;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Database");

        var courses = JsonSerializer.Deserialize<List<Course>>(
            await File.ReadAllTextAsync(Path.Combine(basePath, "courses.json")), options)!;
        await context.Courses.AddRangeAsync(courses);

        var sessions = JsonSerializer.Deserialize<List<ClassSession>>(
            await File.ReadAllTextAsync(Path.Combine(basePath, "classsessions.json")), options)!;
        await context.ClassSessions.AddRangeAsync(sessions);

        var activities = JsonSerializer.Deserialize<List<Activity>>(
            await File.ReadAllTextAsync(Path.Combine(basePath, "activities.json")), options)!;
        await context.Activities.AddRangeAsync(activities);

        var materials = JsonSerializer.Deserialize<List<CourseMaterial>>(
            await File.ReadAllTextAsync(Path.Combine(basePath, "coursematerials.json")), options)!;
        await context.CourseMaterials.AddRangeAsync(materials);

        await context.SaveChangesAsync();
    }
}