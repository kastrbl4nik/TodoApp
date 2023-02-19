using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Models;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<User> Users => this.repository.Users;

        public User Save(User user) { 
            return this.repository.Add(user);
        }
        /*
        public void AddTodoList(User user, TodoList todoList)
        {
            _ = user.TodoLists.Append(todoList);
            this.repository.Update(user);
        }*/
    }
}