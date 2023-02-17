using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface ICrudRepository<out T> where T : class
    {
        IEnumerable<T> FindAll();
    }
}
