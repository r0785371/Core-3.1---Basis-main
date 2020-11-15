using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Models;
using DAL;
using DAL.interfaces;
using BLL;
using BLL.interfaces;

namespace AssetBeheerPortOfAntwerp
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews(o => o.Filters.Add
            (new AuthorizeFilter()));

            //localization
            services.AddLocalization(Options => Options.ResourcesPath = "Resources");
            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("fr")
                 };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = "https://localhost:5000";

                options.ClientId = "AssetBeheerPOA";
                //opslaan in application secrets in productie
                options.ClientSecret = "";
                options.CallbackPath = "/signin-oidc";
                //tot hier

                options.Scope.Add("AssetBeheerPOA");
                options.Scope.Add("AssetBeheerPOA_api");

                options.SaveTokens = true;

                options.GetClaimsFromUserInfoEndpoint = true;
                //mapping zelf aangemaakte claims
                options.ClaimActions.MapUniqueJsonKey("CareerStarted", "CareerStarted");
                options.ClaimActions.MapUniqueJsonKey("FullName", "FullName");
                options.ClaimActions.MapUniqueJsonKey("Role", "Role");
                options.ClaimActions.MapUniqueJsonKey("Permission", "Permission");

                options.ResponseType = "code";
                options.ResponseMode = "form_post";

                options.UsePkce = true;

                options.TokenValidationParameters.RoleClaimType = "Role";

            });

            services.AddIdentity<ApplicationUser , IdentityRole>(
                options => {
                    //opties login
                    options.SignIn.RequireConfirmedAccount = true;
                    //opties paswoord
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 14;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    //lockout
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    

                    })                
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();         

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddIdentityServer()
                .AddInMemoryIdentityResources
                (Configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                ;

            services.AddAuthorization(Options =>
            {
                Options.AddPolicy("Administrator",
                    policy => policy.RequireRole("Administrator"));
            });




            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            //Globalization
            //var cultures = new List<CultureInfo> {
            //          new CultureInfo("en"),
            //          new CultureInfo("fr")
            //};
            //app.UseRequestLocalization(options => {
            //    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
            //    options.SupportedCultures = cultures;
            //    options.SupportedUICultures = cultures;
            //});
            app.UseRequestLocalization(app.ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
