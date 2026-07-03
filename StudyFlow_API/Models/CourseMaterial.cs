namespace StudyFlow;

public class CourseMaterial
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public int ClassSessionId { get; set; }
    public ClassSession? ClassSession { get; set; }
    public required string Title { get; set; }
    public required string BlobRef { get; set; }
    public string? ExtractedTopic { get; set; }
    public int? ExtractedDifficulty { get; set; }
    public int? EstimatedStudyTime { get; set; }

}