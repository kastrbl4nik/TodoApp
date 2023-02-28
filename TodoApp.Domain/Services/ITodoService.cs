using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Domain.Services
{
    public interface ITodoService
    {
        public IQueryable<Todo> Todos { get; }

        public Todo Add(Todo todo);

        public Todo Update(Todo todo);

        public Todo Delete(Todo todo);
    }
}
