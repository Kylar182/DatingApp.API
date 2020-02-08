using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
  public class UserForUpdateDTO
  {
    public string Introduction { get; set; }
    public Gender LoogingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string StateProv { get; set; }
    public Country Country { get; set; }    
  }
}