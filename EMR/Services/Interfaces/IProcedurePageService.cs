using EMR.ViewModels;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IProcedurePageService : IBasePageService<ProcedureViewModel>
    {
        public IEnumerable<ProcedureViewModel> LoadTable(ProcedureSearchModel searchParameters);
        public int GetRecordId(int procedureId);
    }
}
