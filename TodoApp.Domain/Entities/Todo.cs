using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Domain.Entities
{
    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; }

        public TodoList? TodoList { get; set; }
    }
}
