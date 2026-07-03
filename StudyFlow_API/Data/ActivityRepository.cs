namespace StudyFlow;

public class ActivityRepository : IActivityRepository
{
    private readonly StudyFlowDbContext _context;

    public ActivityRepository(StudyFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<Activity>> GetAllAsync() =>
        await _context.Activities.Include(a => a.Course).ToListAsync();

    public async Task<List<Activity>> GetByCourseIdAsync(int courseId) =>
        await _context.Activities
            .Where(a => a.CourseId == courseId)
            .Include(a => a.Course)
            .ToListAsync();

    public async Task AddAsync(Activity activity)
    {
        await _context.Activities.AddAsync(activity);
        await _context.SaveChangesAsync();
    }
}