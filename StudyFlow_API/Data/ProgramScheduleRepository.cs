namespace StudyFlow;

public class ProgramScheduleRepository : IProgramScheduleRepository
{
    private readonly StudyFlowDbContext _context;

    public ProgramScheduleRepository(StudyFlowDbContext context)
    {
        _context = context;
    }

    public async Task<ProgramSchedule?> GetLatestAsync() =>
        await _context.ProgramSchedules
            .OrderByDescending(ps => ps.GeneratedAt)
            .FirstOrDefaultAsync();

    public async Task AddAsync(ProgramSchedule schedule)
    {
        await _context.ProgramSchedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }
}