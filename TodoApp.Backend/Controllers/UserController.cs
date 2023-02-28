using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Extensions;
using TodoApp.Domain.Models;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "User")]
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

        [HttpGet("{username}")]
        public ActionResult<UserModel> GetUser(string username)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault()?.ToModel();

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{username}/todoLists")]
        public ActionResult<TodoListModel> GetTodoLists(string username, bool? hidden, string? nameLike)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var query = this.todoListService.TodoLists
                .Where(tl => tl.User.Username == username);

            if (hidden != null)
            {
                query = query.Where(tl => tl.Hidden == hidden);
            }

            if (nameLike != null)
            {
                query = query.Where(tl => tl.Title.Contains(nameLike));
            }

            var lists = query.Select(tl => tl.ToModel());

            return Ok(lists);
        }

        [HttpPost("{username}/todoLists")]
        public ActionResult<TodoListModel> CreateTodoList(string username, TodoListModel input)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = input.ToEntity(user);

            return CreateActionResult(() => this.todoListService.Add(todoList).ToModel());
        }

        [HttpPut("{username}/todoLists/{id}")]
        public ActionResult<TodoListModel> UpdateTodoList(string username, int id, TodoListModel input)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.FirstOrDefault(tl => tl.Id == id);

            if (todoList is null)
            {
                return NotFound();
            }

            todoList.Title = input.Title;
            todoList.Description = input.Description;
            todoList.Hidden = input.Hidden;

            return CreateActionResult(() => this.todoListService.Update(todoList).ToModel());
        }

        [HttpDelete("{username}/todoLists/{id}")]
        public ActionResult<TodoListModel> DeleteTodoList(string username, int id)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.FirstOrDefault(tl => tl.Id == id);

            if (todoList is null)
            {
                return NotFound();
            }

            return CreateActionResult(() => this.todoListService.Delete(todoList).ToModel());
        }

        [HttpGet("{username}/todoLists/{listId}")]
        public ActionResult<TodoListModel> GetTodoList(string username, int listId)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var todoList = this.todoListService.TodoLists
                .Where(tl => tl.User.Username == username && tl.Id == listId).FirstOrDefault()?.ToModel();

            if (todoList is null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        [HttpGet("{username}/todoLists/{listId}/todos")]
        public ActionResult<IEnumerable<TodoModel>> GetTodos(string username, int listId)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.Where(tl => tl.Id == listId).FirstOrDefault();

            if (todoList is null)
            {
                return NotFound();
            }

            var todos = this.todoService.Todos
                .Where(t => t.TodoList.User.Username == username && t.TodoList.Id == listId).Select(t => t.ToModel()).ToList();

            return Ok(todos);
        }

        [HttpPost("{username}/todoLists/{listId}/todos")]
        public ActionResult<TodoModel> CreateTodo(string username, int listId, TodoModel input)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.Where(tl => tl.Id == listId).FirstOrDefault();

            if (todoList is null)
            {
                return NotFound();
            }

            var todo = input.ToEntity(todoList);

            return CreateActionResult(() => this.todoService.Add(todo).ToModel());
        }

        [HttpPut("{username}/todoLists/{listId}/todos/{id}")]
        public ActionResult<TodoModel> UpdateTodo(string username, int listId, int id, TodoModel input)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.FirstOrDefault(u => u.Username == username);

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.FirstOrDefault(tl => tl.Id == listId);

            if (todoList is null)
            {
                return NotFound();
            }

            var todo = this.todoService.Todos.FirstOrDefault(t => t.Id == id);

            if (todo is null)
            {
                return NotFound();
            }

            todo.DueDate = input.DueDate;
            todo.Title = input.Title;
            todo.Completed = input.Completed;

            return CreateActionResult(() => this.todoService.Update(todo).ToModel());
        }

        [HttpDelete("{username}/todoLists/{listId}/todos/{id}")]
        public ActionResult<TodoModel> DeleteTodo(string username, int listId, int id)
        {
            if (User.Identity?.Name != username)
            {
                return Forbid();
            }

            var user = this.userService.Users.FirstOrDefault(u => u.Username == username);

            if (user is null)
            {
                return NotFound();
            }

            var todoList = this.todoListService.TodoLists.FirstOrDefault(tl => tl.Id == listId);

            if (todoList is null)
            {
                return NotFound();
            }

            var todo = this.todoService.Todos.FirstOrDefault(t => t.Id == id);

            if (todo is null)
            {
                return NotFound();
            }

            return CreateActionResult(() => this.todoService.Delete(todo).ToModel());
        }

        private ActionResult<T> CreateActionResult<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
