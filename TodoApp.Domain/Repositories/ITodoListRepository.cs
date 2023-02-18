using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoListRepository
    {
        IQueryable<TodoList> TodoLists { get; }

        TodoList Add(TodoList todoList);
    }
}
