using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface ITreatmentService : IBusinessService<RecordTreatment>
    {
        public int GetDrugRecordId(int drugId);
        public int GetProcedureRecordId(int procedureId);
    }


}
