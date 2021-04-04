using System.Net.Mime;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using VanillaReact.Api;
using VanillaReact.Api.Contexts;
using Microsoft.EntityFrameworkCore;

namespace VanillaReact.Tests
{
  public class IntegrationTest
  {
    protected readonly HttpClient TestClient;

    protected IntegrationTest()
    {
      var appFactory = new WebApplicationFactory<Startup>()
        .WithWebHostBuilder(builder =>
        {
          builder.ConfigureServices(services =>
          {
            services.RemoveAll(typeof(ApplicationDbContext));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
              options.UseInMemoryDatabase("TestDb");
            });
          });
        });
      TestClient = appFactory.CreateClient();
    }
  }
}
