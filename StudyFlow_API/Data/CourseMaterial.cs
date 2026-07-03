namespace StudyFlow;

public class CourseMaterialRepository : ICourseMaterialRepository
{
    private readonly StudyFlowDbContext _context;

    public CourseMaterialRepository(StudyFlowDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseMaterial>> GetAllAsync() =>
        await _context.CourseMaterials.Include(cm => cm.Course).ToListAsync();

    public async Task<List<CourseMaterial>> GetByCourseIdAsync(int courseId) =>
        await _context.CourseMaterials
            .Where(cm => cm.CourseId == courseId)
            .Include(cm => cm.Course)
            .ToListAsync();

    public async Task<CourseMaterial?> GetByIdAsync(int id) =>
        await _context.CourseMaterials.FindAsync(id);

    public async Task AddAsync(CourseMaterial material)
    {
        await _context.CourseMaterials.AddAsync(material);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CourseMaterial material)
    {
        _context.CourseMaterials.Update(material);
        await _context.SaveChangesAsync();
    }
}