using Microsoft.AspNetCore.Authorization;
using Umbraco.Commerce.Extensions;
using UmbracoDemoIdeas.Configuration;
using UmbracoDemoIdeas.Core;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.CreateUmbracoBuilder()
            .AddUmbracoCommerce()
            .AddBackOffice()
            .AddWebsite()
            .RegisterCore()
            .AddComposers()
            .AddOurSwagger().Build();


        builder.Services.AddControllers();

        var app = builder.Build();

        await app.BootUmbracoAsync();
        app.UseOurSwagger();
        app.MapControllers().WithMetadata(new AllowAnonymousAttribute());
        app.UseHttpsRedirection()
            .UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
                u.UseWebsite();
            })
            .WithEndpoints(u =>
            {
                u.UseBackOfficeEndpoints();
                u.UseWebsiteEndpoints();
            });

        await app.RunAsync();
    }
}