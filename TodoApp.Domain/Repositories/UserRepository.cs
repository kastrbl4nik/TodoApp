using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoAppDbContext context;

        public UserRepository(TodoAppDbContext ctx)
        {
            this.context = ctx;
        }

        public IQueryable<User> Users => this.context.Users;

        public User Add(User user)
        {
            var newUser = this.context.Users.Add(user);
            this.context.SaveChanges();
            return newUser.Entity;
        }
    }
}
