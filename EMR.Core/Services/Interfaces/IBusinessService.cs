using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IBusinessService<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByColumn(string column, string value);
        T GetById(int id);
        void Create(T model);
        void Update(T Model);
        void Delete(int id);
    }
}