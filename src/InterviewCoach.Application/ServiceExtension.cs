using FluentValidation;
using InterviewCoach.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
