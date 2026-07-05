namespace StudyFlow;

public class Activity
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course? Course { get; set; }
    public required string Title { get; set; }
    public ActivityType Type { get; set; }
    public DateTime DueDate { get; set; }
    public int EstimatedEffort { get; set; }
}

public enum ActivityType
{
    Exam,
    Homework,
    Project,
    Quiz
}