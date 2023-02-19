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
        private readonly TodoService todoService;

        public UserController(UserService userService, TodoListService todoListService, TodoService todoService)
        {
            this.userService = userService;
            this.todoListService = todoListService;
            this.todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            var users = this.userService.Users
                .Select(u => u.ToModel())
                .ToList();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<UserModel> CreateUser(UserModel input)
        {
            var user = new User
            {
                Username = input.Username
            };

            var savedUser = this.userService.Save(user).ToModel();
            return Ok(savedUser);
        }

        [HttpGet("{username}")]
        public ActionResult<UserModel> GetUser(string username)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault()?.ToModel();

            if(user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpGet("{username}/todoLists")]
        public ActionResult<TodoListModel> GetTodoLists(string username, bool? hidden)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if(user is null)
            {
                return NotFound();
            }

            var query = this.todoListService.TodoLists
                .Where(tl => tl.User.Username == username);

            if(hidden != null) 
            {
                query = query.Where(tl => tl.Hidden == hidden);
            }

            var lists = query.Select(tl => tl.ToModel());

            return Ok(lists);
        }
        
        [HttpPost("{username}/todoLists")]
        public ActionResult<IEnumerable<TodoListModel>> CreateTodoList(string username, TodoListModel input)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = new TodoList
            {
                Title = input.Title,
                Hidden = input.Hidden,
                User = user,
            };

            var savedTodoList = this.todoListService.Save(todoList).ToModel();

            return Ok(savedTodoList);
        }

        [HttpGet("{username}/todoLists/{title}")]
        public ActionResult<TodoListModel> GetTodoList(string username, string title) 
        {
            var todoList = this.todoListService.TodoLists
                .Where(tl => tl.User.Username == username && tl.Title == title).FirstOrDefault()?.ToModel();

            if(todoList is null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        [HttpGet("{username}/todoLists/{title}/todos")]
        public ActionResult<IEnumerable<TodoModel>> GetTodos(string username, string title)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.Where(tl => tl.Title == title).FirstOrDefault();

            if (todoList is null)
            {
                return NotFound();
            }

            var todos = this.todoService.Todos
                .Where(t => t.TodoList.User.Username == username && t.TodoList.Title == title).Select(t => t.ToModel()).ToList();

            return Ok(todos);
        }
        
        [HttpPost("{username}/todoLists/{title}/todos")]
        public ActionResult<TodoModel> CreateTodo(string username, string title, TodoModel input)
        {
            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.Where(tl => tl.Title == title).FirstOrDefault();

            if (todoList is null)
            {
                return NotFound();
            }

            var todo = input.ToEntity(todoList);

            var savedTodo = this.todoService.Save(todo).ToModel();

            return Ok(savedTodo);
        }
    }
}
