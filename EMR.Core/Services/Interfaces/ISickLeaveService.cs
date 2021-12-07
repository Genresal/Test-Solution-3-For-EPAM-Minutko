using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface ISickLeaveService : IBusinessService<SickLeave>
    { 
        public void Create(SickLeave model, int recordId);
    }
}
