using System;
using System.ComponentModel.DataAnnotations;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
  public class UserForRegisterDTO
  {
    [StringLength(35, MinimumLength = 6, ErrorMessage = "You must specify a username between 6 and 35 characters")]
    public string Username { get; set; }
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,35}$", ErrorMessage = "Password must contain at least 1 Uppercase letter, 1 Lowercase letter, 1 special character, and 1 number")]
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Introduction { get; set; }
    public Gender LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string StateProv { get; set; }
    public string CountryId { get; set; }
  }
}