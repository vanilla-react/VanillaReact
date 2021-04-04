using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
  [Index(nameof(Email), IsUnique = true)]
  public class User
  {
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }

    public List<Snippet> Snippets { get; set; }
    public List<Kudo> Kudos { get; set; }

    public static User Create(string email, string nickname)
    {
      User user = new User();
      user.Email = email;
      user.Nickname = nickname;

      return user;
    }
  }
}