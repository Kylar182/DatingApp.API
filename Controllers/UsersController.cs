using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DatingApp.API.Dtos;
using AutoMapper;
using System.Security.Claims;
using DatingApp.API.Models;
using System;

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

    [HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdateDTO)
		{
      if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        return Unauthorized();

			var userFromRepo = await _repo.GetUser(id);

      _mapper.Map(userForUpdateDTO, userFromRepo);

      if (await _repo.SaveAll())
        return NoContent();

      throw new Exception($"Updating user {id} {userForUpdateDTO} failed on save");
		}
  }
}