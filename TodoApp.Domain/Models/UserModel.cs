using System.ComponentModel.DataAnnotations;

namespace TodoApp.Domain.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        public UserModel(string username)
        {
            Username = username;
        }
    }
}
