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
using Microsoft.ML.OnnxRuntime;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //SETS UP DEPLOYMENT
            var server = Environment.GetEnvironmentVariable("RDS_SERVER");
            var server1 = Environment.GetEnvironmentVariable("RDS_SERVER1");
            var port = Environment.GetEnvironmentVariable("RDS_PORT");
            var port1 = Environment.GetEnvironmentVariable("RDS_PORT1");
            var password = Environment.GetEnvironmentVariable("RDS_PASSWORD");
            var password1 = Environment.GetEnvironmentVariable("RDS_PASSWORD1");
            var database = Environment.GetEnvironmentVariable("RDS_DATABASE");
            var database1 = Environment.GetEnvironmentVariable("RDS_DATABASE1");
            var user = Environment.GetEnvironmentVariable("RDS_USER");
            var user1 = Environment.GetEnvironmentVariable("RDS_USER1");

            //DEFAULT CONNECTION
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql("server=" + server1 + ";port=" + port1 + ";database=" + database1 + ";user=" + user1 + ";password=" + password1));

            //cairo testing connection
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            //CRASH CONNECTION
            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql("server=" + server + ";port=" + port + ";database=" + database + ";user=" + user + ";password=" + password);
            });

            //old connection string
            //services.AddDbContext<CrashDbContext>(options =>
            //    options.UseSqlite(Configuration["ConnectionStrings:CrashDbConnection"]));

            //PASSWORD OPTIONS
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;
            
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //BETA LOGIN FOR BLAZOR IDENTITY

            //services.AddDbContext<AppIdentityDBContext>(options =>
            //{
            //    options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]);
            //});

            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 4;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = false;
            //    options.SignIn.RequireConfirmedAccount = true;
            //})

            //     .AddEntityFrameworkStores<AppIdentityDBContext>()
            //     .AddDefaultTokenProviders();

            //SETS COOKIES
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //GOOGLE AUTHENTICATION THAT WE HAD WORKING IN DEVELOPMENT BUT NOT IN PRODUCTION
            //services.AddAuthentication();
            //    .AddGoogle(options =>
            //    {
            //        IConfigurationSection googleAuthNSection =
            //            Configuration.GetSection("Authentication:Google");
            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    });

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddServerSideBlazor();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });


            // ONNX FILE CONNECTIONS
            ////prediction1
            //services.AddSingleton<InferenceSession>(
            //    new InferenceSession("wwwroot/crash_severity.onnx"));

            ////prediction2
            //services.AddSingleton<InferenceSession>(
            //    new InferenceSession("wwwroot/distracted_crash_severity.onnx"));

            ////prediction4
            //services.AddSingleton<InferenceSession>(
            //    new InferenceSession("wwwroot/age_crash_severity.onnx"));

            //////prediction3
            ////services.AddSingleton<InferenceSession>(
            ////    new InferenceSession("wwwroot/time_crash_severity.onnx"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                //USE COOKIES
                app.UseCookiePolicy();
            }
            else
            {
                app.UseExceptionHandler("/Error");
               
                app.UseHsts();
            }

            //possible problem here: NOT FOR PRODUCTION
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //XSS PROTECTION
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });

            //CSP HEADER
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy-Report-Only",
                    "default-src 'self'; report-uri /cspreport");
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            //USE HSTS
            app.UseHsts();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Inference}/{action=PredictionForm}/{id}");

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

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
                

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");
                
            });

            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}