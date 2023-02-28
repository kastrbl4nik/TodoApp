using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Domain.Entities
{
    public class TodoList
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Hidden { get; set; }

        public List<Todo> Todos { get; set; } = new();

        public User? User { get; set; }
    }
}
