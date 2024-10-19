using Microsoft.EntityFrameworkCore;
using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Infrastructure;

namespace ZD.WikiFuzz.Web;

public static class Program
{
    private static void AddDbContext(IServiceCollection services)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlite("Data Source=:memory:");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSingleton<IGenerateIndexCommand, GenerateIndexCommand>();
    }

    public static async Task Main()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        AddDbContext(builder.Services);
        AddServices(builder.Services);

        WebApplication app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        await app.RunAsync();
    }
}