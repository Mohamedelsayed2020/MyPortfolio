using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IGenericRepositry<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
