using dotnet_bot_accountant.Engine.Managers;
using dotnet_bot_accountant.Engine.TgBot;
using dotnet_bot_accountant.Extensions;
using Serilog;
using Serilog.Events;

namespace dotnet_bot_accountant;

public class Program
{
    public static void Main(string[] args)
    {
        Paths.MakePaths();

        LogExtensions.SetupLogger();

        SettingsManager.Init();

        TgBotManager.Init();

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog();

        builder.WebHost.UseUrls(Shared.Settings.Service.Url);

        // Add services to the container.
        builder.Services.AddControllersWithViews().AddNewtonsoftJson();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}