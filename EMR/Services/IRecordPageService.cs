using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IRecordPageService
    {
        public IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters);
    }
}
