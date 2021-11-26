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
        IEnumerable<T> GetByColumn(string column, string value);
        IEnumerable<T> GetByColumn(string column, List<string> values);
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }


}
