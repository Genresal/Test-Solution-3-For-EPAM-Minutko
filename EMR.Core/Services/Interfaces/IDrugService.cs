using EMR.Business.Models;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IDrugService : IBusinessService<Drug>
    {
        IEnumerable<Drug> GetDrugsForRecord(int RecordId);
        public void Create(Drug model, int recordId);
    }
}
