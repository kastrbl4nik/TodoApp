using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoAppDbContext context;

        public TodoListRepository(TodoAppDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<TodoList> TodoLists => this.context.TodoLists;

        public TodoList Add(TodoList todoList)
        {
            var savedTodoList = this.context.TodoLists.Add(todoList);
            this.context.SaveChanges();
            return savedTodoList.Entity;
        }
    }
}
