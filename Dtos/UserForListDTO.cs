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
    public Country Country { get; set; }
    public string PhotoURL { get; set; }
  }
}