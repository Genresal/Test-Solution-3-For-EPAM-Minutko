using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public interface IRecordService
    {
        IEnumerable<Record> GetAll();
        Record FindById(int id);
        void Create(Record model);
        void Update(Record Model);
        void Delete(int id);
    }
}