using InterviewCoach.Application.Abstractions;
using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using InterviewCoach.Infrastructure.Persistence.Identity;
using InterviewCoach.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace InterviewCoach.Infrastructure.Persistence
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptors>();

            services.AddDbContext<ApplicationContext>((builder, options) =>
            {
                options.AddInterceptors(builder.GetService<ISaveChangesInterceptor>()!);
                options.UseSqlServer(configuration.GetConnectionString(""), builder =>
                {
                    builder.EnableRetryOnFailure(
                           maxRetryCount: 5,
                           maxRetryDelay: TimeSpan.FromSeconds(30),
                           errorNumbersToAdd: null);

                    builder.CommandTimeout(60);
                }).EnableDetailedErrors()
                  .EnableSensitiveDataLogging();
            });
            return services;
        }
    }
}
