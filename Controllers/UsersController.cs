using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DatingApp.API.Dtos;
using AutoMapper;

namespace DatingApp.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
	[ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IDatingRepository _repo;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public UsersController(IDatingRepository repo, IConfiguration config, IMapper mapper)
    {
      _repo = repo;
      _config = config;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _repo.GetUsers();

      var usersTo = _mapper.Map<IEnumerable<UserForListDTO>>(users);

      return Ok(usersTo);
    }

    [HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _repo.GetUser(id);

      var userTo = _mapper.Map<UserDTO>(user);

      return Ok(userTo);
		}
  }
}