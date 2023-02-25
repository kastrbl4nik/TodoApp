using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Models
{
    public class UserModel
    {
        public UserModel(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
