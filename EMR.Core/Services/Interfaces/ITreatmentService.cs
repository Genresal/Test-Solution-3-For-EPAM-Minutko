using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface ITreatmentService : IBusinessService<RecordTreatment>
    {
        public int GetDrugRecordId(int drugId);
        public int GetProcedureRecordId(int procedureId);
    }


}
