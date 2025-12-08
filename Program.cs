using LipometryAppAPI.Data;
using LipometryAppAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Lipometry API",
        Version = "v1"
    });
});

// Add authorization services
builder.Services.AddAuthorization();

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Fallback for development
    connectionString = "Server=localhost;Database=LipometryDB;Trusted_Connection=True;TrustServerCertificate=True;";
}

// Add DbContext with configuration
builder.Services.AddDbContext<LipometryContext>(options =>
{
    options.UseSqlServer(connectionString);

    // Enable sensitive data logging only in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }
});

// Register repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Database setup for development
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LipometryContext>();

        try
        {
            // Delete and recreate for clean development (optional)
            //context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            // Optional: Seed initial data
            // await SeedData.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the database.");
            throw;
        }
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lipometry API v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    // In production, ensure database exists without deleting
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LipometryContext>();
        try
        {
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred ensuring database exists.");
            throw;
        }
    }
}

// Middleware order is important!
app.UseHttpsRedirection();

// This now works because we called builder.Services.AddAuthorization()
app.UseAuthorization();

app.MapControllers();

app.Run();