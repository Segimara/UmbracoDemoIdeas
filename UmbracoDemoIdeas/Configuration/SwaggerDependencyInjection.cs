namespace UmbracoDemoIdeas.Configuration;

public static class SwaggerDependencyInjection
{
    public static IUmbracoBuilder AddOurSwagger(this IUmbracoBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {

        });

        return builder;
    }
    public static IApplicationBuilder UseOurSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
