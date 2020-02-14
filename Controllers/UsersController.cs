using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DatingApp.API.Dtos;
using AutoMapper;
using System.Security.Claims;
using System;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
  [ServiceFilter(typeof(UserActivity))]
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
    public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
    {
      var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
      var user = await _repo.GetUser(userId);

      userParams.UserId = userId;      
      userParams.Gender = userParams.Gender ?? user.LookingFor;
      userParams.Country = userParams.Country ?? user.Country;
      userParams.OrderBy = userParams.OrderBy ?? false;

      var users = await _repo.GetUsers(userParams);

      var usersTo = _mapper.Map<IEnumerable<UserForListDTO>>(users);

      Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, 
                users.TotalPages);

      return Ok(usersTo);
    }

    [HttpGet("{id}", Name="GetUser")]
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

      throw new Exception($"Updating user {id} {userForUpdateDTO.ToString()} failed on save");
		}

    [HttpPost("{id}/like/{recipientId}")]
		public async Task<IActionResult> Like(int id, int recipientId)
		{
      if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        return Unauthorized();

			if (await _repo.GetUser(recipientId) == null)
        return NotFound(recipientId);

      var like = await _repo.GetLike(id, recipientId);

      if (like != null)
        return BadRequest("You've already liked this user");

      like = new Like
      {
        LikerId = id,
        LikeeId = recipientId
      };

      _repo.Add<Like>(like);

      if (await _repo.SaveAll())
        return Ok();

      return BadRequest("Failed to like user");
		}
  }
}