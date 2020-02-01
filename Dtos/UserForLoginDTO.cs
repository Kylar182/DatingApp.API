using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
  public class UserForLoginDTO
  {
    [StringLength(35, MinimumLength = 6, 
      ErrorMessage = "You must specify a username between 6 and 35 characters")]
    public string Username { get; set; }
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", 
      ErrorMessage = "Password must contain at least 1 Uppercase letter, 1 Lowercase letter, 1 special character, and 1 number min/max 8/16")]
    public string Password { get; set; }
  }
}