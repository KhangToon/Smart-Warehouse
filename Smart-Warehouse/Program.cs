using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smart_Warehouse.Data;
namespace SmartWarehouse
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Radzen;
    using Smart_Warehouse.API.Models;
    using Smart_Warehouse.Commons;
    using Smart_Warehouse.Data;
    using Smart_Warehouse.Services;
    using Smart_Warehouse.Services.PLCServices;
    using Smart_Warehouse.Services.QRCodes;
    using Smart_Warehouse.Services.SQLService;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    public static class Program
    {
        public static void Main(string[] args)
        {
            // Find an available port
            var localIP = PortFinder.GetLocalIPAddress();
            IPAddress localIPAddress = IPAddress.Parse(localIP);
            var port = PortFinder.FindAvailablePort_CheckUsed(localIPAddress);
            var url = $"http://{localIP}:{port}";

            var builder = WebApplication.CreateBuilder(args);

            // Identity config
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DBConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); ;

                options.UseSqlServer(connectionString);
            });

            // Identity config (auto create after scaffold) // using when use default identity
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>();

            // Password options
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            // For API 
            builder.Services.AddControllers();
            // For API 
            builder.Services.AddHttpClient(Common.ServerAPI, client =>
            {
                client.BaseAddress = new Uri($"{url}/");
            });
            // For API 
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient(Common.ServerAPI));

            builder.Services.AddScoped<PLCAPIService>();

            // Add Radzen components
            builder.Services.AddRadzenComponents();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            // Blazor bootrstap
            builder.Services.AddBlazorBootstrap();
            // Radzen services
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            // Authentication 
            builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            // DB services
            builder.Services.AddSingleton<SQLServerServices>();
            // QR code services
            builder.Services.AddSingleton<QRCodeServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // Add authen/author services
            app.UseAuthentication();
            app.UseAuthorization();

            // For API 
            app.MapControllers(); // Map API controllers
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.MapRazorPages();

            // Open browser after application starts
            app.Urls.Add(url);
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
            ///////////

            app.Run();
        }
    }
}
