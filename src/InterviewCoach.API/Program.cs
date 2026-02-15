using InterviewCoach.API.Extensions;
using InterviewCoach.Application;
using InterviewCoach.Infrastructure.Persistence;
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
            builder.Services.AddPresentation();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceService(builder.Configuration);
      
            WebApplication app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerWithUI();
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRequestContextLogging();

            app.UseSerilogRequestLogging();

            app.UseExceptionHandler();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.MapControllers();
           
            await app.RunAsync();
        }
    }
}