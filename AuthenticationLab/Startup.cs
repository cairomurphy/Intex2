using AuthenticationLab.Data;
using AuthenticationLab.Models;
using AuthenticationLab.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //no touchy
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CrashDbConnection"]);
            });
 
            //no touchy
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddDbContext<AppIdentityDBContext>(options =>
            //{
            //    options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]);
            //});

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //     .AddEntityFrameworkStores<AppIdentityDBContext>();

            // Sets the display of the Cookie Consent banner (/Pages/Shared/_CookieConsentPartial.cshtml).
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        IConfigurationSection googleAuthNSection =
            //            Configuration.GetSection("Authentication:Google");
            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    });

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseCookiePolicy();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //security
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                "Content-Security-Policy: ");
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHsts();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "countynamepage",
                    "{COUNTY_NAME}/Page={pageNum}",
                    new { Controller = "Home", action = "Data" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page={pageNum}",
                    defaults: new { Controller = "Home", action = "Data", pageNum = 1 });

                endpoints.MapControllerRoute(
                    "countyname",
                    "{COUNTY_NAME}",
                    new { Controller = "Home", action = "Data", pageNum = 1 });

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
                endpoints.MapDefaultControllerRoute();
            });

            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}