using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<User> Users => this.repository.Users;

        public User Add(User user) { 
            if (this.repository.Users.Where(u => u.Username == user.Username).Any())
            {
                throw new InvalidOperationException("User with this username already exists");
            }
            return this.repository.Add(user);
        }
    }
}