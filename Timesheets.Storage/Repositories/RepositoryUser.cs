using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Storage.Repositories
{
    public class RepositoryUser : Repository<User>, IUserRepository
    {
        public RepositoryUser(TimeSheetDbContext context) : base(context) { }

        public override async Task<User> GetById(int Id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);

        async Task<User> IUserRepository.GetByUsername(string username) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
