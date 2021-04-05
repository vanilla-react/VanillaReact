using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using VanillaReact.Api.Models;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace VanillaReact.Api
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
            services.Configure<CookiePolicyOptions>(opts =>
            {
                opts.CheckConsentNeeded = context => true;
                opts.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddAuthentication(opts =>
            {
                opts.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.SlidingExpiration = true;
            }).AddGitHub("Github", options =>
            {
                options.ClientId = "e24f83ece8cd1f73ed95"; // client id from registering github app
                options.ClientSecret = "d0c9aa50ac8cbf5ff0b3a864ff0a52a9b12a1747";
                options.Scope.Add("user:email");
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = OnCreatingGitHubTicket()
                };
            });
            services.AddScoped<UserService>();
            services.AddDbContext<ApplicationDbContext>(
              options => options.UseSqlite("Data Source=/tmp/VanillaReact.db"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VanillaReact.Api", Version = "v1" });
            });
        }

        private Func<OAuthCreatingTicketContext, Task> OnCreatingGitHubTicket()
        {
            return async context =>
            {

                var fullName = context.Identity.FindFirst("urn:github:name").Value;
                var email = context.Identity.FindFirst(ClaimTypes.Email).Value;
                //Todo: Add logic here to save info into database
                // this Task.FromResult is purely to make the code compile as it requires a Task result
                await Task.FromResult(true);
            };
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VanillaReact.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
