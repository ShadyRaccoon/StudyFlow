using Microsoft.EntityFrameworkCore;
using Models;

namespace StudyFlow;

public class StudyFlowDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ClassSession> ClassSessions { get; set; }
    public DbSet<CourseMaterial>  CourseMaterials { get; set; }
    public DbSet<ProgramSchedule>  ProgramSchedules { get; set; }
}