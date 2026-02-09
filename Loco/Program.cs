
using System.Globalization;
using Loco.Infrastructure.Persistence;
using Loco.Localization.Configuration;
using Loco.Localization.Resources;
using Loco.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;


namespace Loco
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DevConnection")
                ?? throw new InvalidOperationException("Connection string 'DevConnection' not found.");

            builder.Services.AddDbContext<LocoDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                // .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LocoDbContext>();

            // MVC + Razor Pages
            builder.Services
                .AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    // Route DataAnnotations to SharedResource
                    options.DataAnnotationLocalizerProvider = (_, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            // register localization layer (ILocalizer + IStringLocalizer<SharedResource>)
            builder.Services.AddAppLocalization();

            builder.Services.AddRazorPages();
            builder.Services.AddSingleton<IAdminStateService, AdminStateService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
                {
                app.UseMigrationsEndPoint();
                }
            else
                {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                }

             

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // request localization middleware
            var supported = new[] { "bg-BG", "en-US" }
                .Select(c => new CultureInfo(c)).ToList();

            app.UseRequestLocalization(new RequestLocalizationOptions
                {
                DefaultRequestCulture = new RequestCulture("bg-BG"),
                SupportedCultures = supported,
                SupportedUICultures = supported,
                RequestCultureProviders = new IRequestCultureProvider[]
                {
                    new CookieRequestCultureProvider(),
                    new QueryStringRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
                });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
            }
        }
    }