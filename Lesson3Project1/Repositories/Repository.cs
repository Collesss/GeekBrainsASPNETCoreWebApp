using Lesson3Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson3Project1.Repositories
{
    public class Repository : IRepository<Person>
    {
        private object _loker = new object();
        private List<Person> _persons = new List<Person>();

        public Repository()
        {

        }

        public Repository(IEnumerable<Person> initData)
        {
            _persons.AddRange(initData);
        }

        bool IRepository<Person>.Add(Person value)
        {
            lock(_loker)
            {
                Person person = _persons.FirstOrDefault(p => p.Id == value.Id);

                if(person == null)
                {
                    _persons.Add(value);
                    return true;
                }

                return false;
            }
        }

        bool IRepository<Person>.Delete(int id)
        {
            lock (_loker)
            {
                Person person = _persons.FirstOrDefault(p => p.Id == id);

                if (person != null)
                {
                    _persons.Remove(person);
                    return true;
                }

                return false;
            }
        }

        bool IRepository<Person>.Update(Person value)
        {
            lock (_loker)
            {
                Person person = _persons.FirstOrDefault(p => p.Id == value.Id);

                if (person != null)
                {
                    person.FirstName = value.FirstName;
                    person.LastName = value.LastName;
                    person.Email = value.Email;
                    person.Company = value.Company;
                    person.Age = value.Age;
                    return true;
                }

                return false;
            }
        }

        IEnumerable<Person> IRepository<Person>.GetAll() =>
            _persons;
    }
}
