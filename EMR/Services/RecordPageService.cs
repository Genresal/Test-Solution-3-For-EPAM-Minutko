using EMR.Business.Services;
using EMR.Mapper;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public class RecordPageService : BaseTableService, IRecordPageService
    {

        IRecordService _recordService;
        IDoctorService _doctorService;

        public RecordPageService(IRecordService s, IDoctorService ss)
        {
            _recordService = s;
            _doctorService = ss;
        }
        public IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters)
        {
            var result = _recordService.GetAll().Select(x => x.ToViewModel()).AsQueryable();

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            if (searchParameters.DateRange != null)
            {
                if (searchParameters.DateRange.Start != "")
                    date1 = DateTime.Parse(searchParameters.DateRange.Start);

                if (searchParameters.DateRange.End != "")
                    date2 = DateTime.Parse(searchParameters.DateRange.End);
            }

            if (date1 != DateTime.MinValue)
                result = result.Where(u => u.ModifiedDate >= date1);

            if (date2 != DateTime.MinValue)
                result = result.Where(u => u.ModifiedDate <= date2);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.PatientName != null && r.PatientName.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }


    }
}
