using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        IQueryable<Todo> Todos { get; }

        Todo Add(Todo todo);

        Todo Update(Todo todo);

        Todo Delete(Todo todo);
    }
}
