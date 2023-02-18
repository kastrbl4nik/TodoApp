using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        IQueryable<Todo> Todos { get; }    
    }
}
