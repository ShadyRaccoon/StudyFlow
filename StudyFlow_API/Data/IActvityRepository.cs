public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<List<Activity>> GetByCourseIdAsync(int courseId);
    Task AddAsync(Activity activity);
}