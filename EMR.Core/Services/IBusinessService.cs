using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public interface IBusinessService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T model);
        void Update(T Model);
        void Delete(int id);
    }
}