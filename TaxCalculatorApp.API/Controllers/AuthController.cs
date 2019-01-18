using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaxCalculatorApp.API.Data;
using TaxCalculatorApp.API.DTOs;
using TaxCalculatorApp.API.models;

namespace TaxCalculatorApp.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper) {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO) {
            userForRegisterDTO.Username = userForRegisterDTO.Username.Trim().ToLower();
            userForRegisterDTO.Email = userForRegisterDTO.Email.Trim().ToLower();

            if(await _repo.UserExists(userForRegisterDTO.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User {
                Username = userForRegisterDTO.Username,
                Email = userForRegisterDTO.Email,
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
            //return CreatedAtRoute();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO) {
            try
            {
                var userFromRepo = await _repo.Login(userForLoginDTO.Username.Trim().ToLower(), userForLoginDTO.Password);

            if(userFromRepo == null)
                return Unauthorized();

            var claims = new [] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToReturn = _mapper.Map<UserToReturnDTO>(userFromRepo);
            return Ok(new 
            {
                token = GenerateJwtToken(userFromRepo),
                user = userToReturn
            });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            

        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username)
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

            return tokenHandler.WriteToken(token);
        }

    }
}