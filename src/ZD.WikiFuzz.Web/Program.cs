using ZD.WikiFuzz.Application.Index.Command;
using ZD.WikiFuzz.Application.Index.Query;

namespace ZD.WikiFuzz.Web;

// Stryker disable all
public static class Program
{
    public static async Task Main()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IGenerateIndexCommand, GenerateIndexCommand>();
        builder.Services.AddSingleton<IGetArticleQuery, GetArticleQuery>();

        WebApplication app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.MapStaticAssets();

        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        await app.RunAsync();
    }
}
// Stryker restore all