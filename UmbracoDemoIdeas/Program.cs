using UmbracoDemoIdeas.Core;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddDeliveryApi()
            .RegisterCore()
            .AddComposers()
            .Build();

        var app = builder.Build();

        await app.BootUmbracoAsync();

        app.UseHttpsRedirection();

        app.UseUmbraco()
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