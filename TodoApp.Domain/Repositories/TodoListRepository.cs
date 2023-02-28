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

        public TodoList Update(TodoList todoList)
        {
            var updatedTodoList = this.context.TodoLists.Update(todoList);
            this.context.SaveChanges();
            return updatedTodoList.Entity;
        }

        public TodoList Delete(TodoList todoList)
        {
            var deletedTodoList = this.context.TodoLists.Remove(todoList);
            this.context.SaveChanges();
            return deletedTodoList.Entity;
        }
    }
}
