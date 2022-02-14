using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timesheets.Storage.Repositories
{
    public interface IRepository<T, BaseKey> where T : BaseKey
    {
        public IQueryable<T> GetAll();
        public bool Add(T value);
        public bool Update(T value);
        public bool Delete(BaseKey key);
    }
}
