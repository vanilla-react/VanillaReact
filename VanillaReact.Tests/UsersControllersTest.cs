using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using VanillaReact.Api.Models;
using System.Net.Http.Json;

namespace VanillaReact.Tests
{
  public class UsersControllersTest : IntegrationTest
  {
    [Fact]
    public async Task ShouldReturnAnEmptyArrayWhenThereAreNoUsers()
    {
      HttpResponseMessage response = await TestClient.GetAsync("/user");

      response.StatusCode.Should().Be(HttpStatusCode.OK);
      var users = await response.Content.ReadFromJsonAsync<List<User>>();
      users.Should().HaveCount(0);
    }
  }
}
