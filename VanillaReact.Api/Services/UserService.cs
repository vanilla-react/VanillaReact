using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;
using VanillaReact.Api.Dtos;

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
            return _ctx.Users.ToList<User>();
        }

        public User Create(CreateUserDto payload)
        {
            User user = User.Create(payload.Email, payload.Nickname);

            _ctx.Users.Add(user);
            _ctx.SaveChanges();

            return user;
        }
    }
}
