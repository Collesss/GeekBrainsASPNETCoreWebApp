using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Storage.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TimeSheetDbContext _context;
        public Repository(TimeSheetDbContext context)
        {
            _context = context;
        }

        IQueryable<T> IRepository<T>.GetAll() =>
            _context.Set<T>().AsNoTracking();

        async Task IRepository<T>.Add(T value)
        {
            await _context.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        async Task IRepository<T>.Delete(T value)
        {
            _context.Remove(value);
            await _context.SaveChangesAsync();
        }

        async Task IRepository<T>.Update(T value)
        {
            _context.Update(value);
            await _context.SaveChangesAsync();
        }
    }
}
