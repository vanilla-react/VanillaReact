using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;


namespace VanillaReact.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly ApplicationDbContext _ctx;

    public UserController(ApplicationDbContext ctx)
    {
      _ctx = ctx;
    }

    [HttpGet]
    public ActionResult<List<User>> Get()
    {
      return _ctx.Users.ToList();
    }
  }
}
