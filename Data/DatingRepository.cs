using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
  {
    private readonly DataContext _context;
    public DatingRepository(DataContext context)
    {
      _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<Photo> GetMain(int userId)
    {
      return await _context.Photos.Where(p => p.UserId == userId && p.IsMain == true)
        .FirstOrDefaultAsync();
    }

    public async Task<Photo> GetPhoto(int id)
    {
      Photo photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

      return photo;
    }

    public async Task<User> GetUser(int id)
    {
      User user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

      return user;
    }

    public async Task<PageList<User>> GetUsers(UserParams userParams)
    {
      var users = _context.Users.Include(p => p.Photos)
                                  .Where(u => u.Id != userParams.UserId
                                  && u.Gender == userParams.Gender
                                  && u.DateOfBirth >= userParams.MaxAge.GetMinDate()
                                  && u.DateOfBirth <= userParams.MinAge.GetMaxDate()
                                  && u.Country == userParams.Country)
                                  .OrderByDescending(u => u.LastActive).AsQueryable();
                                  
      if(userParams.OrderBy == true)
      {
        users = users.OrderByDescending(u => u.Created);
      }

      return await PageList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}