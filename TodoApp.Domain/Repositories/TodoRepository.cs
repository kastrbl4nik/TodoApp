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

        public Todo Update(Todo todo)
        {
            var updatedTodo = this.context.Todos.Update(todo);
            this.context.SaveChanges();
            return updatedTodo.Entity;
        }

        public Todo Delete(Todo todo)
        {
            var deletedTodo = this.context.Todos.Remove(todo);
            this.context.SaveChanges();
            return deletedTodo.Entity;
        }
    }
}
