using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Domain.Models;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly TodoListService todoListService;

        public UserController(UserService userService, TodoListService todoListService)
        {
            this.userService = userService;
            this.todoListService = todoListService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            var users = this.userService.Users
                .Select(u => u.ToModel())
                .ToList();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<UserModel> Save(UserModel input)
        {
            var user = new User
            {
                Username = input.Username
            };

            var savedUser = this.userService.Save(user).ToModel();
            return Ok(savedUser);
        }

        [HttpGet("{username}")]
        public ActionResult<UserModel> GetByUsername(string username)
        {
            var user = this.userService.GetByUsername(username)?.ToModel();

            if(user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpGet("{username}/todoLists")]
        public ActionResult<TodoListModel> GetTodoLists(string username)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if(user is null)
            {
                return NotFound();
            }

            var lists = this.todoListService.TodoLists
                .Where(tl => tl.User.Username == username)
                .Select(l => l.ToModel())
                .ToList();

            return Ok(lists);
        }
        
        [HttpPost("{username}/todoLists")]
        public ActionResult<IEnumerable<TodoListModel>> AddTodoList(string username, TodoListModel input)
        {
            var user = this.userService.GetByUsername(username);

            if (user is null)
            {
                return NotFound();
            }

            var todoList = new TodoList
            {
                Title = input.Title,
                User = user
            };

            var savedTodoList = this.todoListService.Save(todoList).ToModel();

            return Ok(savedTodoList);
        }
    }
}
