namespace StudyFlow;

public class ClassSessionRepository : IClassSessionRepository
{
    private readonly StudyFlowDbContext _context;

    public ClassSessionRepository(StudyFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClassSession>> GetAllAsync() =>
        await _context.ClassSessions.Include(cs => cs.Course).ToListAsync();

    public async Task<List<ClassSession>> GetByCourseIdAsync(int courseId) =>
        await _context.ClassSessions
            .Where(cs => cs.CourseId == courseId)
            .Include(cs => cs.Course)
            .ToListAsync();

    public async Task AddAsync(ClassSession session)
    {
        await _context.ClassSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }
}