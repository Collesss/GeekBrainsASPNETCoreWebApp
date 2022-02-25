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
        public Task<T> Add(T value);
        public Task<T> Update(T value);
        public Task<T> Delete(T value);
    }
}
