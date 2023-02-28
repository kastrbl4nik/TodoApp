using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public interface ITodoListService
    {
        IQueryable<TodoList> TodoLists { get; }

        TodoList Add(TodoList todoList);

        TodoList Update(TodoList todoList);

        TodoList Delete(TodoList todoList);
    }
}
