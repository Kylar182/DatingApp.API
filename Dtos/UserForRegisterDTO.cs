using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
  public class UserForRegisterDTO
  {
    [StringLength(35, MinimumLength = 6, ErrorMessage = "You must specify a username between 6 and 35 characters")]
    public string Username { get; set; }
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,35}$", ErrorMessage = "Password must contain at least 1 Uppercase letter, 1 Lowercase letter, 1 special character, and 1 number")]
    public string Password { get; set; }
  }
}