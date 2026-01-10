using LipometryAppAPI.Data;
using LipometryAppAPI.Database;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAthleteService, AthleteService>();
            services.AddScoped<IBodyMeasurementRepository, BodyMeasurementRepository>();
            services.AddScoped<IBodyMeasurementService, BodyMeasurementService>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString =
                    "Server=localhost;Database=LipometryDB;Trusted_Connection=True;TrustServerCertificate=True;";
            }
            services.AddDbContext<LipometryContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
