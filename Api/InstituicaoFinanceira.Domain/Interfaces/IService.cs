using System.Collections.Generic;

namespace InstituicaoFinanceira.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        T Post(T obj);

        T Put(T obj);

        void Delete(int id);

        T Get(int id);

        IList<T> GetAll();
    }
}
