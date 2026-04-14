using InterviewCoach.API.Extensions;
using InterviewCoach.Application;
using InterviewCoach.Infrastructure.Persistence;
using Scalar.AspNetCore;
using Serilog;


namespace InterviewCoach.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });
            builder.Services.AddPresentation(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceService(builder.Configuration);

            WebApplication app = builder.Build();

            app.UseSwaggerWithUI();
            if (app.Environment.IsDevelopment())
            {
                //app.MapSwagger("/openapi/{documentName}.json");
                //app.MapScalarApiReference();
                app.UseDeveloperExceptionPage();
            }
            // Remove or comment these two out — you don't have HTTPS cert on IIS yet
            // app.UseHsts();
            // app.UseHttpsRedirection();

            app.UseRequestContextLogging();

            app.UseSerilogRequestLogging();

            app.UseExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // TEMPORARY — remove before going live
            app.MapGet("/debug/env", () => new
            {
                Environment = app.Environment.EnvironmentName,
                IsDevelopment = app.Environment.IsDevelopment(),
                IsProduction = app.Environment.IsProduction(),
                ASPNETCORE_ENVIRONMENT = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            }).AllowAnonymous();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}