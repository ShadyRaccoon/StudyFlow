public interface ICourseMaterialRepository
{
    Task<List<CourseMaterial>> GetAllAsync();
    Task<List<CourseMaterial>> GetByCourseIdAsync(int courseId);
    Task<CourseMaterial?> GetByIdAsync(int id);
    Task AddAsync(CourseMaterial material);
    Task UpdateAsync(CourseMaterial material); // needed for when Stage 1 writes back extracted metadata
}