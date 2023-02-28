using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository repository;

        public TodoService(ITodoRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<Todo> Todos => this.repository.Todos;

        public Todo Add(Todo todo)
        {
            if (this.repository.Todos.Where(t => t.Id == todo.Id).Any())
            {
                throw new InvalidOperationException("Todo already exists");
            }
            return this.repository.Add(todo);
        }

        public Todo Update(Todo todo)
        {
            if (!this.repository.Todos.Where(t => t.Id == todo.Id).Any())
            {
                throw new InvalidOperationException("Todo does not exist");
            }
            return this.repository.Update(todo);
        }

        public Todo Delete(Todo todo)
        {
            if (!this.repository.Todos.Where(t => t.Id == todo.Id).Any())
            {
                throw new InvalidOperationException("Todo does not exist");
            }
            return this.repository.Delete(todo);
        }
    }
}
