using LipometryAppAPI.Data;
using LipometryAppAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Add a DbContext here
//builder.Services.AddDbContext<LipometryContext>();

builder.Services.AddDbContext<LipometryContext>();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IAthleteRepository, AthleteRepository>();

var app = builder.Build();

////fix in future
//var scope = app.Services.CreateScope();
//var contect = scope.ServiceProvider.GetRequiredService<LipometryContext>();
//contect.Database.EnsureDeleted();
//contect.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lipometry API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at root path
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();