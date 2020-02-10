using System;
using System.ComponentModel.DataAnnotations;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
  public class UserForRegisterDTO
  {
    [StringLength(35, MinimumLength = 6, ErrorMessage = "You must specify a username between 6 and 35 characters")]
    [RegularExpression(@"^[A-Za-z0-9.\s_-]+$", ErrorMessage = "Username can only contain uppercase/lowercase, periods, spaces and dashes")]
    public string Username { get; set; }
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,16}$", ErrorMessage = "Password must contain at least 1 Uppercase letter, 1 Lowercase letter, 1 special character, and 1 number")]
    public string Password { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a first name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "First name can only contain uppercase/lowercase letters")]
    public string FirstName { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a last name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Last name can only contain uppercase/lowercase letters")]
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a nick name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Nick name can only contain uppercase/lowercase letters")]
    public string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a city name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City name can only contain uppercase/lowercase letters")]
    public string City { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a state or province name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "State or Province name can only contain uppercase/lowercase letters")]
    public string StateProv { get; set; }
    public Country Country { get; set; }
  }
}