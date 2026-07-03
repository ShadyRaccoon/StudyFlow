public interface IClassSessionRepository
{
    Task<List<ClassSession>> GetAllAsync();
    Task<List<ClassSession>> GetByCourseIdAsync(int courseId);
    Task AddAsync(ClassSession session);
}