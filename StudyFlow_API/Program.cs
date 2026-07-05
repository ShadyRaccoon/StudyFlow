using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IClassSessionRepository, ClassSessionRepository>();
builder.Services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IProgramScheduleRepository, ProgramScheduleRepository>();
builder.Services.AddScoped<AnthropicService>();
builder.Services.AddDbContext<StudyFlowDbContext>(options =>
    options.UseSqlite("Data Source=../Database/studyflow.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StudyFlowDbContext>();
    await StudyFlowSeeder.SeedAsync(context);
}

app.MapOpenApi();
app.MapScalarApiReference();
app.UseCors();
app.MapControllers();

app.Run();