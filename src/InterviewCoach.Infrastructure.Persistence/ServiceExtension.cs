using InterviewCoach.Infrastructure.Persistence.Database;
using InterviewCoach.Infrastructure.Persistence.Interceptors;
using InterviewCoach.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewCoach.Infrastructure.Persistence
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditEntityInterceptors>();

            services.AddDbContext<ApplicationContext>((builder, options) =>
            {
                options.AddInterceptors(builder.GetService<ISaveChangesInterceptor>()!);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.EnableRetryOnFailure(
                           maxRetryCount: 5,
                           maxRetryDelay: TimeSpan.FromSeconds(30),
                           errorNumbersToAdd: null);

                    builder.CommandTimeout(60);
                }).EnableDetailedErrors()
                  .EnableSensitiveDataLogging();
            });
            services.AddSingleton<ISystemClock, SystemClock>();
            services.AddScoped<IPageReadRepository, PageReadRepository>();
            services.AddScoped<IPageWriteRepository, PageWriteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITechnologyReadRepository, TechnologyReadRepository>();
            services.AddScoped<ITechnologyWriteRepository, TechnologyWriteRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicReadRepository, TopicReadRepository>();

            return services;
        }
    }
}
