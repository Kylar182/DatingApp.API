using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DatingApp.API.Dtos;

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

      IEnumerable<UserForListDTO> userDTOs = UserForListDTO.BuildList(users);

      return Ok(userDTOs);
    }

    [HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _repo.GetUser(id);

      return Ok(new UserDTO(user));
		}
  }
}