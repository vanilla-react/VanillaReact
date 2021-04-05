using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;
using VanillaReact.Api.Dtos;
using VanillaReact.Api.Services;
using Microsoft.AspNetCore.Authentication;


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
    public IActionResult GithubLogin()
    {
        return Challenge(new AuthenticationProperties{ RedirectUri = "/Account" }, "Github");

    }
  }
}
