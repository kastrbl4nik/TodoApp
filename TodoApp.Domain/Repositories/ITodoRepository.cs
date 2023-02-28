using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        IQueryable<Todo> Todos { get; }

        Todo Add(Todo todo);

        Todo Update(Todo todo);

        Todo Delete(Todo todo);
    }
}
