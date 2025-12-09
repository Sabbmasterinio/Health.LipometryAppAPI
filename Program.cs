using LipometryAppAPI.Data;
using LipometryAppAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------
// Configuration
// ------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialize enums as strings
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------
// Database (EF Core)
// ------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString =
        "Server=localhost;Database=LipometryDB;Trusted_Connection=True;TrustServerCertificate=True;";
}

builder.Services.AddDbContext<LipometryContext>(options =>
{
    options.UseSqlServer(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }
});

// ------------------------------------
// Repositories
// ------------------------------------
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();

builder.Services.AddAuthorization();

var app = builder.Build();

// ------------------------------------
// Development configuration
// ------------------------------------
if (app.Environment.IsDevelopment())
{
    // Create DB if needed (no drop)
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<LipometryContext>();
    context.Database.EnsureCreated();

    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Production: ensure DB exists safely
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<LipometryContext>();
    context.Database.EnsureCreated();
}

// ------------------------------------
// Middleware pipeline
// ------------------------------------
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
