using Loco.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Loco
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);

            // 1) DbContext към DevConnection
            var connectionString = builder.Configuration.GetConnectionString("DevConnection")
                ?? throw new InvalidOperationException("Connection string 'DevConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // 2) Identity
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    // За локална разработка - по-бързо без потвърждение на акаунт
                    options.SignIn.RequireConfirmedAccount = false;
                })
                // .AddRoles<IdentityRole>() // ако ще ползваме роли
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // 3) MVC + Razor Pages (за Identity UI)
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // 4) Pipeline
            if (app.Environment.IsDevelopment())
                {
                app.UseMigrationsEndPoint();
                }
            else
                {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // 30 дни по подразбиране
                }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
 
            app.UseAuthentication();
            app.UseAuthorization();

            // 5) Endpoints
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); // Identity UI

            app.Run();
            }
        }
    }