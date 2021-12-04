using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RecordTreatmentService : BaseBusinessService<RecordTreatment>, ITreatmentService
    {
        public RecordTreatmentService(IRepository<RecordTreatment> r) : base (r)
        {
        }

        public int GetDrugRecordId(int drugId)
        {
            int recordId = _mainRepository.GetByColumn(nameof(RecordTreatment.DrugId), drugId.ToString()).FirstOrDefault().RecordId;
            return recordId;
        }

        public int GetProcedureRecordId(int procedureId)
        {
            int recordId = _mainRepository.GetByColumn(nameof(RecordTreatment.ProcedureId), procedureId.ToString()).FirstOrDefault().RecordId;
            return recordId;
        }
    }
}
