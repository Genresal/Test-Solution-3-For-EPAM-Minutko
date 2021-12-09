using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface ISickLeaveService : IBusinessService<SickLeave>
    { 
        public void Create(SickLeave model, int recordId);
    }
}
