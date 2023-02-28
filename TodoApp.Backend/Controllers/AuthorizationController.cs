using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoApp.Backend.Exceptions;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Domain.Models;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthorizationController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<UserModel> Register(AuthorizationModel input)
        {
            if(input.Password.IsNullOrEmpty())
            {
                return BadRequest("Password cannot be empty");
            }

            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = input.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            try
            {
                var savedUser = userService.Add(user);
                return Ok(savedUser.ToModel());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login(AuthorizationModel input)
        {
            var user = userService.Users.Where(u => u.Username == input.Username).FirstOrDefault();

            if (user is null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(input.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User"),
            };

            var authKey = this.configuration.GetSection("Authorization:Key").Value;
            if(authKey is null)
            {
                throw new ConfigurationException("Authorization key cannot be null");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(authKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
