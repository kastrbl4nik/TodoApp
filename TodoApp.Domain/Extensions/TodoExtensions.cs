using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;

namespace TodoApp.Domain.Extensions
{
    public static class TodoExtensions
    {
        public static TodoModel ToModel(this Todo todo)
        {
            return new TodoModel(todo.Id, todo.Title, todo.DueDate, todo.Completed);
        }

        public static Todo ToEntity(this TodoModel todoModel, TodoList todoList)
        {
            return new Todo
            {
                Title = todoModel.Title,
                DueDate = todoModel.DueDate,
                Completed = todoModel.Completed,
                TodoList = todoList,
            };
        }
    }
}
