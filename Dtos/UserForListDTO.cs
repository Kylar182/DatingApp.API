using System;
using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
  public class UserForListDTO
  {
    public string Username { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string City { get; set; }
    public string StateProv { get; set; }
    public string CountryId { get; set; }
    public string PhotoURL { get; set; }

    public UserForListDTO() { }

    public UserForListDTO(User usr)
    {
      Username = usr.Username;
      FirstName = usr.FirstName;
      LastName = usr.LastName;
      Gender = usr.Gender;
      Age = usr.DateOfBirth.GetAge();
      KnownAs = usr.KnownAs;
      Created = usr.Created;
      LastActive = usr.LastActive;
      City = usr.City;
      StateProv = usr.StateProv;
      CountryId = usr.CountryId;
      PhotoURL = usr.Photos.Where(im => im.IsMain == true).Select(url => url.Url).First();
    }

    public static IEnumerable<UserForListDTO> BuildList(IEnumerable<User> users)
    {
      var list = new List<UserForListDTO>();

      foreach(var user in users)
        list.Add(new UserForListDTO(user));

      return list;
    }
  }
}