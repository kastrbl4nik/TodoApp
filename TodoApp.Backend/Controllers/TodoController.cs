using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Services;

namespace TodoApp.Backend.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService todoService;

        public TodoController(TodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet]
        public ActionResult<Todo> GetAll()
        {
            var todo = todoService.GetAll();
            return Ok(todo);
        }
    }
}
