using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface IDrugService : IBusinessService<Drug>
    { 
        IEnumerable<Drug> GetDrugsForRecord(int RecordId);
    }
}
