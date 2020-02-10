using System.ComponentModel.DataAnnotations;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
  public class UserForUpdateDTO
  {
    [MaxLengthAttribute(250, ErrorMessage = "Your introduction cannot be longer than 250 characters")]
    public string Introduction { get; set; }
    public Gender LoogingFor { get; set; }

    [MaxLengthAttribute(500, ErrorMessage = "Your Interests cannot be longer than 500 characters")]
    public string Interests { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a city name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City name can only contain uppercase/lowercase letters")]
    public string City { get; set; }

    [StringLength(35, MinimumLength = 2, ErrorMessage = "You must specify a state or province name between 2 and 35 characters")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "State or Province name can only contain uppercase/lowercase letters")]
    public string StateProv { get; set; }
    public Country Country { get; set; }    
  }
}