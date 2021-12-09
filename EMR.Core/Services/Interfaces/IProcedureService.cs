using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IProcedureService : IBusinessService<Procedure>
    {
        IEnumerable<Procedure> GetProceduresForRecord(int RecordId);
        public void Create(Procedure model, int recordId);
    }


}
