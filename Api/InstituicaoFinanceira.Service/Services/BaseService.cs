using InstituicaoFinanceira.Domain.Interfaces;
using InstituicaoFinanceira.Infra.Data.Repository;
using System;
using System.Collections.Generic;

namespace InstituicaoFinanceira.Service.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private BaseRepository<T> repository = new BaseRepository<T>();
        public void Delete(int id)
        {
            if (id == 0)
                throw new ArgumentException("O código não pode ser 0.");

            repository.Remove(id);
        }

        public T Get(int id)
        {
            if (id == 0)
                throw new ArgumentException("O código não pode ser 0.");

            return repository.Select(id);
        }

        public IList<T> GetAll()
        {
            return repository.SelectAll() ;
        }

        public T Post(T obj)
        {
            repository.Insert(obj);
            return obj;
        }

        public T Put(T obj)
        {
            repository.Update(obj);
            return obj;
        }
    }
}
