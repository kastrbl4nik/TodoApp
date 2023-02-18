using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Models
{
    public class TodoListModel
    {
        public string Title { get; set; }

        public bool Hidden { get; set; }

        public TodoListModel(string title, bool hidden)
        {
            Title = title;
            Hidden = hidden;
        }
    }
}
