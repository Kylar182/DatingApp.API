using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
	[ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IDatingRepository _repo;
    private readonly IConfiguration _config;

    public UsersController(IDatingRepository repo, IConfiguration config)
    {
      _repo = repo;
      _config = config;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _repo.GetUsers();

      var userDTOs = new List<UserDTO>();

      foreach(var user in users)
      {
        userDTOs.Add(user.UserToVM());
      }

      return Ok(userDTOs);
    }

    [HttpGet("{id}")]
		public async Task<IActionResult> GetValue(int id)
		{
			var user = await _repo.GetUser(id);
      return Ok(user.UserToVM());
		}
  }
}