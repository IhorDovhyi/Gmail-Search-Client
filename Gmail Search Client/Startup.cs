using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Gmail_Search_Client
{
    public class Startup
    {
        private const string _clientSecretFilenameKey = "appsettings.json";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            var clientSecrets = GoogleClientSecrets.FromFile(_clientSecretFilenameKey).Secrets;

            services
                .AddAuthentication(o =>
                {
                    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                })
                .AddGoogleOpenIdConnect(options =>
                {
                    options.ClientId = clientSecrets.ClientId;
                    options.ClientSecret = clientSecrets.ClientSecret;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

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
