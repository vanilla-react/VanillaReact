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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace VanillaReact.Api
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
      services.Configure<CookiePolicyOptions>(opts =>
      {
        opts.CheckConsentNeeded = context => true;
        opts.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
      });

      services.ConfigureApplicationCookie(o =>
        {
          o.Events = new CookieAuthenticationEvents()
          {
            OnRedirectToLogin = (ctx) =>
            {
              ctx.Response.StatusCode = 403;
              return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = (ctx) =>
            {
              ctx.Response.StatusCode = 403;
              return Task.CompletedTask;
            }
          };
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
        options.Events.OnRedirectToAccessDenied =
        options.Events.OnRedirectToLogin = c =>
            {
              c.Response.StatusCode = StatusCodes.Status401Unauthorized;
              return Task.FromResult<object>(null);
            };
      }).AddGitHub("Github", options =>
      {
        options.ClientId = "e24f83ece8cd1f73ed95"; // client id from registering github app
        options.ClientSecret = "d0c9aa50ac8cbf5ff0b3a864ff0a52a9b12a1747";
        options.Scope.Add("user:email");
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
