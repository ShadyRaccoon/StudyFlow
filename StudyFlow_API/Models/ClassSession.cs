namespace StudyFlow;

public class ClassSession
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course?  Course { get; set; }
    public SessionType?  SessionType { get; set; }
    public System.DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}

public enum SessionType
{
    Course,
    Laboratory,
    Seminar
}