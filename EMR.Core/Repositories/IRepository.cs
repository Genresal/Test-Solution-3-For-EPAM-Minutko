using EMR.Business.Models;
using System.Collections.Generic;

namespace EMR.Business.Repositories
{
    public interface IRepository
    {
        public void SetDefaultData();
        void DropTable();
        bool IsTableExist();
        bool IsTableHasRecords();
    }

    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="T">Business Model class</typeparam>
    public interface IRepository<T> : IRepository where T : BaseModel
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByColumn(string column, int value);
        IEnumerable<T> GetByColumn(string column, string value);
        IEnumerable<T> GetByColumn(string column, List<string> values);
        int GetLastId();
        T GetById(int id);
        void Create(T item, int relationId);
        void Create(T item);
        void Update(T item);
        void Update(string column, string value, int id);
        void Delete(int id);
    }


}
