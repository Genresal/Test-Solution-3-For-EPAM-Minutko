using EMR.ViewModels;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IDrugPageService : IBasePageService<DrugViewModel>
    {
        public IEnumerable<DrugViewModel> LoadTable(DrugSearchModel searchParameters);
        public int GetRecordId(int drugId);
    }
}
