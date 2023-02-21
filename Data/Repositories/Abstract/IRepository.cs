using System;
using Core.Entities;

namespace Data.Repositories.Abstract
{
	public interface IRepository<T> where T : BaseEntitiy
	{
        List<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}

