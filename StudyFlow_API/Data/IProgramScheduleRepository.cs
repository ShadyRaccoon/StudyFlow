public interface IProgramScheduleRepository
{
    Task<ProgramSchedule?> GetLatestAsync();
    Task AddAsync(ProgramSchedule schedule);
}