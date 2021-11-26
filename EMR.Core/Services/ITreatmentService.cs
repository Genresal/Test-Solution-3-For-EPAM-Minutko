using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface ITreatmentService
    {
        IEnumerable<Drug> GetAllDrugs(int RecordId);
        IEnumerable<Procedure> GetAllProcedures(int RecordId);
    }


}
