using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using InterviewCoach.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewCoach.Infrastructure.Persistence
{
    public static class ServiceExtension
    {
        public static void ConfigurePersistenceService(this IServiceCollection services, string DbConnectionString)
        {
            services.AddDbContext<ApplicationContext>((builder, options) =>
            {
                options.AddInterceptors(builder.GetService<ISaveChangesInterceptor>()!);
                options.UseSqlServer(DbConnectionString, builder =>
                {
                    builder.EnableRetryOnFailure(
                           maxRetryCount: 5,
                           maxRetryDelay: TimeSpan.FromSeconds(30),
                           errorNumbersToAdd: null);

                    builder.CommandTimeout(60);
                }).EnableDetailedErrors()
                  .EnableSensitiveDataLogging();
            });

            services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptors>();
        }
    }
}
