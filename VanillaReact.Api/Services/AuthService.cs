using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using VanillaReact.Api.Contexts;
using VanillaReact.Api.Models;
using VanillaReact.Api.Dtos;

namespace VanillaReact.Api.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserService _userService;
        public AuthService(ApplicationDbContext ctx, UserService service)
        {
            _ctx = ctx;
            _userService = service;
        }

    }
}
