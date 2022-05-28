using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Storage.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username);
    }
}
