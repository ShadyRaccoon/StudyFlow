var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IClassSessionRepository, ClassSessionRepository>();
builder.Services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IProgramScheduleRepository, ProgramScheduleRepository>();

var app = builder.Build();

builder.Services.AddDbContext<StudyFlowDbContext>(options =>
    options.UseInMemoryDatabase("StudyFlowDb"));

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StudyFlowDbContext>();
    await StudyFlowSeeder.SeedAsync(context);
}
