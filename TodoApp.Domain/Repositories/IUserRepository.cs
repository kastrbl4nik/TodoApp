using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        
        User Add(User user);

        //User Update(User user);
    }
}
