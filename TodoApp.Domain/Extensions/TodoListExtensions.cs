using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            return new TodoListModel(todoList.Id, todoList.Title, todoList.Description, todoList.Hidden, todoList.CreatedDate);
        }

        public static TodoList ToEntity(this TodoListModel todoListModel, User user)
        {
            return new TodoList
            {
                Title = todoListModel.Title,
                Description = todoListModel.Description,
                Hidden = todoListModel.Hidden,
                User = user
            };
        }
    }
}
