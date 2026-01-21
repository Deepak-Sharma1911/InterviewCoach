using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewCoach.Application
{
    public static class ServiceExtension
    {
        public static void ConfigureApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(Behavious.UnitOfWorkBehavior<,>));
            });
        }
    }
}
