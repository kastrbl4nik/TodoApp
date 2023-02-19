using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Models
{
    public class TodoModel
    {
        public TodoModel(string title, string? description, DateTime? dueDate, bool completed)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Completed = completed;
        }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; }

    }
}
