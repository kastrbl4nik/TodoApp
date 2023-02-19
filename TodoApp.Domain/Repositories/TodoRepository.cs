using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext context;

        public TodoRepository(TodoAppDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<Todo> Todos => this.context.Todos;

        public Todo Add(Todo todo)
        {
            var savedTodo = this.context.Todos.Add(todo);
            this.context.SaveChanges();
            return savedTodo.Entity;
        }
    }
}
