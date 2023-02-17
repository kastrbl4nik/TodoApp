﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<User> GetAll()
        {
            var user = this.service.GetAll();
            return Ok(user);
        }
    }
}
