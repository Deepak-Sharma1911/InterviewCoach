using InterviewCoach.API.Extensions;
using InterviewCoach.API.Services;
using InterviewCoach.Application;
using InterviewCoach.Application.Abstractions;
using InterviewCoach.Infrastructure.Persistence;
using Serilog;

namespace InterviewCoach.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });
            builder.Services.AddPresentation();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceService(builder.Configuration);

            WebApplication app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRequestContextLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}