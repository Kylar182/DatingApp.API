using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace DatingApp.API.Controllers
{
  [Route("api/[controller]")]
	[ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
    {
      _repo = repo;
      _config = config;
      _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegisterDTO usr)
    {
      usr.Username = usr.Username.TrimFix();
      usr.FirstName = usr.FirstName.TrimFix();
      usr.LastName = usr.LastName.TrimFix();
      usr.KnownAs = usr.KnownAs.TrimFix();
      usr.City = usr.City.TrimFix();
      usr.StateProv = usr.StateProv.TrimFix();
      if (usr.Username != null)
        usr.Username = usr.Username.ToLower();
      else 
        return BadRequest("Username cannot be null");

      if (await _repo.UserExists(usr.Username))
        return BadRequest("Username already exists");

      var userToCreate = new User 
      {
        Username = usr.Username
      };

      var createdUser = await _repo.Register(usr);

      var userToReturn = _mapper.Map<UserDTO>(createdUser);

      return CreatedAtRoute("GetUser", new {controller = "Users", id = createdUser.Id}, userToReturn);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLoginDTO usr)
    {
      usr.Username = usr.Username.TrimFix();
      if (usr.Username != null)
        usr.Username = usr.Username.ToLower();
      else 
        return BadRequest("Username cannot be null");

      User userFromRepo = await _repo.Login(usr.Username, usr.Password);

      if (userFromRepo == null)
        return Unauthorized();

      var claims = new []
      {
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
        new Claim(ClaimTypes.Name, userFromRepo.Username)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      var user = _mapper.Map<UserForListDTO>(userFromRepo);

      return Ok(new {
        token = tokenHandler.WriteToken(token),
        user
      });
    }
  }
}