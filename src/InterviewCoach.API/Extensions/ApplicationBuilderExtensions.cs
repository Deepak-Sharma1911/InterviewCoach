namespace InterviewCoach.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
            return builder;
        }
    }
}
