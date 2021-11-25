using System;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    public interface IRepository
    {
        public void CreateDefaultDate();
        void DropTable();
        bool IsTableExist();
        bool IsTableHasRecords();
    }

    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="T">Business Model class</typeparam>
    public interface IRepository<T> : IRepository
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }


}
