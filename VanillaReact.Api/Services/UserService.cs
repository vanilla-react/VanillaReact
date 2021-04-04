using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;

namespace VanillaReact.Api.Services
{
  public class UserService
  {
    private readonly ApplicationDbContext _ctx;

    public UserService(ApplicationDbContext ctx)
    {
      _ctx = ctx;
    }

    public List<User> GetAll()
    {
      return this._ctx.Users.ToList<User>();
    }
  }
}
