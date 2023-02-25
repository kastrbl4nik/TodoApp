using System.Security.Cryptography;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.Extensions
{
    public static class UserExtensions
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel(user.Username);
        }
    }
}
