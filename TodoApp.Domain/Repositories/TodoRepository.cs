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
    }
}
