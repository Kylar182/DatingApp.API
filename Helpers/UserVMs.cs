using System.Collections.Generic;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
  public static class UserVMs
  {
    public static UserDTO UserToVM(this User usr)
    {
      if (usr != null)
      {
        UserDTO userDTO = new UserDTO()
        {
          Username = usr.Username
        };

        return userDTO; 
      }
      return null;
    }

    public static List<UserDTO> UsersToVM(this List<User> usrs)
    {
      if (usrs.Count > 0)
      {
        List<UserDTO> userDTOs = new List<UserDTO>();

        foreach (User usr in usrs) 
        {
          UserDTO userDTO = new UserDTO()
          {
            Username = usr.Username
          };

          userDTOs.Add(userDTO);
        }

        return userDTOs;
      }
      return null;
    }
  }
}