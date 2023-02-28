using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoListRepository
    {
        IQueryable<TodoList> TodoLists { get; }

        TodoList Add(TodoList todoList);

        TodoList Update(TodoList todoList);

        TodoList Delete(TodoList todoList);
    }
}
