using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        
        User Add(User user);
    }
}
