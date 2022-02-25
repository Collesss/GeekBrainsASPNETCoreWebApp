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

        async Task<T> IRepository<T>.Add(T value)
        {
            var entiry = await _context.AddAsync(value);
            await _context.SaveChangesAsync();
            return entiry.Entity;
        }

        async Task<T> IRepository<T>.Delete(T value)
        {
            var entiry = _context.Remove(value);
            await _context.SaveChangesAsync();
            return entiry.Entity;
        }

        async Task<T> IRepository<T>.Update(T value)
        {
            var entiry = _context.Update(value);
            await _context.SaveChangesAsync();
            return entiry.Entity;
        }
    }
}
