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
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.TodoLists)
                .WithOne(tl => tl.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TodoList>()
                .HasMany(tl => tl.Todos)
                .WithOne(t => t.TodoList)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<TodoList>()
                .HasIndex(tl => tl.Title)
                .IsUnique();
        }
    }
}
