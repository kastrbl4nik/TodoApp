using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options) { }

        public DbSet<TodoList> TodoLists => Set<TodoList>();
        public DbSet<Todo> Todos => Set<Todo>();
    }
}
