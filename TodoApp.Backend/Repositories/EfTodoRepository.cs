using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Backend.Repositories
{
    public class EfTodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext context;

        public EfTodoRepository(TodoAppDbContext ctx)
        {
            this.context = ctx;
        }

        public IEnumerable<Todo> FindAll()
        {
            return this.context.Todos;
        }
    }
}
