using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Services
{
    public abstract class BaseBusinessService<T> where T : BaseModel
    {
        protected IRepository<T> _mainRepository;
        protected BaseBusinessService(IRepository<T> r)
        {
            _mainRepository = r;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _mainRepository.GetAll();
        }

        public virtual IEnumerable<T> GetByColumn(string column, string value)
        {
            return _mainRepository.GetByColumn(column, value);
        }

        public T GetById(int id)
        {
            return _mainRepository.GetById(id);
        }

        public virtual void Create(T model)
        {
            _mainRepository.Create(model);
        }

        public virtual void Update(T model)
        {
            _mainRepository.Update(model);
        }

        public virtual void Delete(int id)
        {
            _mainRepository.Delete(id);
        }
    }
}
