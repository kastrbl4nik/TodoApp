using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Backend
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options) { }

        public DbSet<TodoList> TodoLists => Set<TodoList>();
        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.TodoList)
                .WithMany(tl => tl.Todos)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
