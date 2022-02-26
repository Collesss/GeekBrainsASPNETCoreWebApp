using System.Collections.Generic;
using System.Linq;

namespace Lesson3Project1.Repositories
{
    public interface IRepository<T, BaseKey> where T : BaseKey
    {
        public List<T> GetAll();
        public bool Add(T value);
        public bool Update(T value);
        public bool Delete(BaseKey key);

        public T GetByKey(BaseKey baseKey);
    }
}
