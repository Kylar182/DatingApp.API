using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
  public interface IAuthRepository
  {
    Task<User> Register(UserForRegisterDTO user);
    Task<User> Login(string username, string password);
    Task<bool> UserExists(string username);
  }
}