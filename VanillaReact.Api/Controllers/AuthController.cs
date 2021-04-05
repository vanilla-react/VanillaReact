using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;
using VanillaReact.Api.Dtos;
using VanillaReact.Api.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace VanillaReact.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly ApplicationDbContext _ctx;

    public AuthController(ApplicationDbContext ctx, UserService userService)
    {
      _ctx = ctx;
    }

    [HttpGet]
    [Route("callback")]
    [Produces("application/json")]
    public ActionResult Callback()
    {
      var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
      var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

      var user = _ctx.Users.Any(u => u.Email == email);
      if (!user)
      {
        _ctx.Users.Add(Api.Models.User.Create(email, username));
        _ctx.SaveChanges();
      }

      return Redirect("http://localhost:3000/");
    }

    [HttpDelete]
    [Route("logout")]
    public async Task Logout()
    {
      await HttpContext.SignOutAsync();
    }

    [HttpGet]
    public IActionResult GithubLogin()
    {
      return Challenge(new AuthenticationProperties { RedirectUri = "/auth/callback" }, "Github");

    }
  }
}
