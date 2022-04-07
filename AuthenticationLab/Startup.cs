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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //no touchy
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:CrashDbConnection"]);
            });

            //services.AddDbContext<CrashDbContext>(options =>
            //    options.UseSqlite(Configuration["ConnectionStrings:CrashDbConnection"]));

            //no touchy
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

            services.AddDbContext<AppIdentityDBContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:IdentityConnection"]);
            });

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

            // Sets the display of the Cookie Consent banner (/Pages/Shared/_CookieConsentPartial.cshtml).
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddServerSideBlazor();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

            services.AddSingleton<InferenceSession>(
                new InferenceSession("C:/Users/murph/source/repos/AuthenticationLab/AuthenticationLab/wwwroot/onnx/crash_severity.onnx"));
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
            //app.Use(async (ctx, next) =>
            //{

            //    ctx.Response.Headers.Add("Content-Security-Policy",
            //    "script src = 'https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js';" +
            //    "script src = '~/ js / scripts.js';" +
            //    "script src = 'https://use.fontawesome.com/releases/v6.1.0/js/all.js';" +
            //    "default-src 'self';" +
            //    "script-src 'self';" +
            //    "style-src 'self';" +
            //    "img-src 'self';" +
            //    "script-src 'self';");
            //    await next();
            //});

            //loose
            app.Use(async (ctx, next) =>
            {
                ctx.Response.Headers.Add("Content-Security-Policy",
                "default-src * data: blob: filesystem: about: ws: wss: 'unsafe-inline' 'unsafe-eval' 'unsafe-dynamic';" +
                "script-src * data: blob: 'unsafe-inline' 'unsafe-eval';" +
                "connect-src * data: blob: 'unsafe-inline';" +
                "img-src * data: blob: 'unsafe-inline';" +
                "frame-src * data: blob:;" +
                "style-src * data: blob: 'unsafe-inline';" +
                "font-src * data: blob: 'unsafe-inline';" +
                "frame-ancestors * data: blob: 'unsafe-inline';");
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

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

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}