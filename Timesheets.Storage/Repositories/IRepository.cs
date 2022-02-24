using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.Storage.Repositories
{
    public interface IRepository<T>
    {
        public IQueryable<T> GetAll();
        public Task Add(T value);
        public Task Update(T value);
        public Task Delete(T value);
    }
}
