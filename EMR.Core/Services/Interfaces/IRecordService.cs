using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IRecordService : IBusinessService<Record>
    {
        public Record GetLast();
    }


}
