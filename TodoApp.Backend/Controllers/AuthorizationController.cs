﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Domain.Models;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserService userService;

        public AuthorizationController(IConfiguration configuration, UserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<UserModel> Register(UserLoginModel input)
        {
            CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = input.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            var savedUser = userService.Save(user);

            return Ok(savedUser.ToModel());
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginModel input)
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

            if (this.configuration.GetSection("Authorization:Token").Value == null) {
                throw new InvalidOperationException("Authorization token cannot be null");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("Authorization:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}