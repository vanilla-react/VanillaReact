using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;
using VanillaReact.Api.Dtos;
using VanillaReact.Api.Services;


namespace VanillaReact.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly ApplicationDbContext _ctx;
    private readonly UserService _userService;

    public UserController(ApplicationDbContext ctx, UserService userService)
    {
      _ctx = ctx;
      _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> Get()
    {
      List<User> users = _userService.GetAll();
      return users;
    }

    [HttpPost]
    public ActionResult<User> Store(CreateUserDto payload)
    {
      return this._userService.Create(payload);
    }
  }
}
