using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IDrugPageService : IBasePageService<Drug>
    {
        public IEnumerable<DrugViewModel> LoadTable(DrugSearchModel searchParameters);
    }
}
