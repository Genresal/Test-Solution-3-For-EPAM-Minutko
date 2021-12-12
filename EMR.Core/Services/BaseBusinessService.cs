using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public abstract class BaseBusinessService<T> where T : BaseModel
    {
        protected readonly IRepository<T> _mainRepository;
        protected readonly ILogger<BaseBusinessService<T>> _logger;
        protected BaseBusinessService(IRepository<T> repository, ILogger<BaseBusinessService<T>> logger)
        {
            _mainRepository = repository;
            _logger = logger;
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
            _logger.LogInformation($"Service created new {typeof(T).Name}");
        }

        public virtual void Update(T model)
        {
            _mainRepository.Update(model);
            _logger.LogInformation($"Service updated new {typeof(T).Name}");
        }

        public virtual void Delete(int id)
        {
            _mainRepository.Delete(id);
            _logger.LogInformation($"Service deleted {typeof(T).Name} with id = {id}");
        }
    }
}
