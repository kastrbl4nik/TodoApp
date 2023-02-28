using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public class TodoService
    {
        private readonly ITodoRepository repository;

        public TodoService(ITodoRepository repository)
        {
            this.repository = repository;
        }

        public IQueryable<Todo> Todos => this.repository.Todos;

        public Todo Add(Todo todo)
        {
            return this.repository.Add(todo);
        }

        public Todo Update(Todo todo)
        {
            return this.repository.Update(todo);
        }

        public Todo Delete(Todo todo)
        {
            return this.repository.Delete(todo);
        }
    }
}
