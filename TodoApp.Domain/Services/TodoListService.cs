using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class TodoListService
    {
        private readonly ITodoListRepository repository;

        public TodoListService(ITodoListRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<TodoList> TodoLists => this.repository.TodoLists;

        public TodoList Add(TodoList todoList)
        {
            return this.repository.Add(todoList);
        }

        public TodoList Update(TodoList todoList)
        {
            return this.repository.Update(todoList);
        }

        public TodoList Delete(TodoList todoList)
        {
            return this.repository.Delete(todoList);
        }
    }
}
