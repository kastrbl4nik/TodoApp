using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository repository;

        public TodoListService(ITodoListRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<TodoList> TodoLists => this.repository.TodoLists;

        public TodoList Add(TodoList todoList)
        {
            if (this.repository.TodoLists.Where(t => t.Id == todoList.Id).Any())
            {
                throw new InvalidOperationException("TodoList already exists");
            }
            return this.repository.Add(todoList);
        }

        public TodoList Update(TodoList todoList)
        {
            if (!this.repository.TodoLists.Where(t => t.Id == todoList.Id).Any())
            {
                throw new InvalidOperationException("TodoList does not exist");
            }
            return this.repository.Update(todoList);
        }

        public TodoList Delete(TodoList todoList)
        {
            if (!this.repository.TodoLists.Where(t => t.Id == todoList.Id).Any())
            {
                throw new InvalidOperationException("TodoList does not exist");
            }
            return this.repository.Delete(todoList);
        }
    }
}
