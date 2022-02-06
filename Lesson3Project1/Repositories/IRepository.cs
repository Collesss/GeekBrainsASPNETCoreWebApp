using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson3Project1.Repositories
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public bool Add(T value);
        public bool Update(T value);
        public bool Delete(int id);
    }
}
