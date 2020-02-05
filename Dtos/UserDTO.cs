using System;
using System.Collections.Generic;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using System.Linq;

namespace DatingApp.API.Dtos
{
  public class UserDTO
  {
    public string Username { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Introduction { get; set; }
    public Gender LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string StateProv { get; set; }
    public string CountryId { get; set; }
    public string PhotoURL { get; set; }
    public ICollection<Photo> Photos { get; set; }

    public UserDTO() { }

    public UserDTO(User usr)
    {
      Username = usr.Username;
      FirstName = usr.FirstName;
      LastName = usr.LastName;
      Gender = usr.Gender;
      Age = usr.DateOfBirth.GetAge();      
      Created = usr.Created;      
      LastActive = usr.LastActive;
      KnownAs = usr.KnownAs;
      Introduction = usr.Introduction;
      LookingFor = usr.LookingFor;
      Interests = usr.Interests;
      City = usr.City;
      StateProv = usr.StateProv;
      CountryId = usr.CountryId;
      PhotoURL = usr.Photos.Where(im => im.IsMain == true).Select(url => url.Url).First();
      Photos = usr.Photos;
    }

    public static IEnumerable<UserDTO> BuildList(IEnumerable<User> users)
    {
      var list = new List<UserDTO>();

      foreach(var user in users)
        list.Add(new UserDTO(user));

      return list;
    }
  }
}