using System;
using System.Collections.Generic;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using System.Linq;

namespace DatingApp.API.Dtos
{
  public class UserDTO
  {
    public int Id { get; set; }
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
    public Country Country { get; set; }
    public string PhotoURL { get; set; }
    public ICollection<Photo> Photos { get; set; }
  }
}