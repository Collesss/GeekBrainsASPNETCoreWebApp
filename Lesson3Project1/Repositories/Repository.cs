﻿using Lesson3Project1.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lesson3Project1.Repositories
{
    public class Repository : IRepository<Person, BaseKey>
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

        bool IRepository<Person, BaseKey>.Add(Person value)
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

        bool IRepository<Person, BaseKey>.Delete(BaseKey key)
        {
            lock (_loker)
            {
                Person person = _persons.FirstOrDefault(p => p.Id == key.Id);

                if (person != null)
                {
                    _persons.Remove(person);
                    return true;
                }

                return false;
            }
        }

        bool IRepository<Person, BaseKey>.Update(Person value)
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

        List<Person> IRepository<Person, BaseKey>.GetAll() =>
            _persons;

        Person IRepository<Person, BaseKey>.GetByKey(BaseKey baseKey) =>
            _persons.FirstOrDefault(p => p.Id == baseKey.Id);
    }
}
