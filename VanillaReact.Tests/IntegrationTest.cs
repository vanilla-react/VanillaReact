using System.Net.Mime;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using VanillaReact.Api;
using VanillaReact.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
              services.Remove(descriptor);
            }

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
