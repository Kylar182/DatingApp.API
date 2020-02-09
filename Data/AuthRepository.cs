using System;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
  public class AuthRepository : IAuthRepository
  {
    private readonly DataContext _context;
    public AuthRepository(DataContext context)
    {
      _context = context;
      
    }
    public async Task<User> Login(string username, string password)
    {
      var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Username == username);

      if (user == null)
        return null;

      if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        return null;

      return user;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computeddHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        
        for(int i = 0; i <computeddHash.Length; i++) 
        {
          if (computeddHash[i] != passwordHash[i])
          return false;
        }
      }
      return true;
    }

    public async Task<User> Register(UserForRegisterDTO vm)
    {
      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(vm.Password.TrimFix(), out passwordHash, out passwordSalt);

      User user = new User();

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      DateTime date = DateTime.UtcNow;

      user.FirstName = vm.FirstName.TrimFix();
      user.LastName = vm.LastName.TrimFix();
      user.Gender = vm.Gender;
      user.DateOfBirth = vm.DateOfBirth;
      user.KnownAs = vm.KnownAs.TrimFix();
      user.Created = date;
      user.LastActive = date;
      user.Introduction = vm.Introduction.TrimFix();
      user.LookingFor = vm.LookingFor;
      user.Interests = vm.Interests.TrimFix();
      user.City = vm.City.TrimFix();
      user.StateProv = vm.StateProv.TrimFix();
      user.Country = vm.Country;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    public async Task<bool> UserExists(string username)
    {
      if (await _context.Users.AnyAsync(un => un.Username == username))
        return true;
      
      return false;
    }
  }
}