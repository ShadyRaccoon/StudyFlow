using Microsoft.EntityFrameworkCore;

namespace StudyFlow;

public class StudyFlowDbContext : DbContext
{
    public StudyFlowDbContext(DbContextOptions<StudyFlowDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ClassSession> ClassSessions { get; set; }
    public DbSet<CourseMaterial>  CourseMaterials { get; set; }
    public DbSet<ProgramSchedule>  ProgramSchedules { get; set; }
}