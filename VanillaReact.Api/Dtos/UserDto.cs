using System.ComponentModel.DataAnnotations;

namespace VanillaReact.Api.Dtos
{
  public class CreateUserDto
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(16)]
    public string Nickname { get; set; }
  }
}