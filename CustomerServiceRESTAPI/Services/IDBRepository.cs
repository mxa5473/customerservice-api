using System;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;

namespace CustomerServiceRESTAPI.Services
{
    public interface IDBRepository<T>
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Save();
    }
}
