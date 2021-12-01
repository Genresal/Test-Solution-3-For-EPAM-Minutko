using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IBasePageService<T>
    {
        public T GetById(int id);
        public void Create(T item);
        public void Update(T item);
        public void Delete(int id);

    }
}
