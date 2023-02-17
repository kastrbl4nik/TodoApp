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
        private readonly ITodoRepository todoRepository;

        public TodoService(ITodoRepository repository)
        {
            this.todoRepository = repository;
        }

        public IEnumerable<Todo> GetAll()
        {
            return this.todoRepository.FindAll();
        }
    }
}
