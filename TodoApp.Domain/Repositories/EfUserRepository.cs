using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public class EfUserRepository : IUserRepository
    {
        private readonly TodoAppDbContext context;

        public EfUserRepository(TodoAppDbContext ctx)
        {
            this.context = ctx;
        }

        public IEnumerable<User> FindAll()
        {
            return this.context.Users;
        }
    }
}
