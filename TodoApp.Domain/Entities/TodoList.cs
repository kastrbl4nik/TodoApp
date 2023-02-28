using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class TodoList
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Hidden { get; set; }

        public List<Todo> Todos { get; set; } = new();

        [Required]
        public User User { get; set; }
    }
}
