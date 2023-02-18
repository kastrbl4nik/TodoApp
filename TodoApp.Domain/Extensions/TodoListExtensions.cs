using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.Extensions
{
    public static class TodoListExtensions
    {
        public static TodoListModel ToModel(this TodoList todoList)
        {
            return new TodoListModel(todoList.Title, todoList.Hidden);
        }
    }
}
