using EMR.Business.Models;
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
        IBusinessService<Record> _recordService;
        IBusinessService<Position> _positionService;

        public RecordPageService(IBusinessService<Record> sr, IBusinessService<Position> sp)
        {
            _recordService = sr;
            _positionService = sp;
        }

        public IEnumerable<Position> GetDoctorPositions()
        {
            return _positionService.GetAll();
        }

        public IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters)
        {
            var result = _recordService.GetAll().Select(x => x.ToViewModel()).AsQueryable();

            if (searchParameters.DoctorPositions.Count > 0)
            {
                var filterParams = searchParameters.DoctorPositions.Select(r => r.Id);
                result = result.Where(r => filterParams.Contains(r.DoctorPositionId));
            }

            if (!string.IsNullOrEmpty(searchParameters.Diagnosis))
            {
                result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchParameters.Diagnosis.ToUpper()));
            }

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
                result = result.Where(r => r.PatientName != null && r.PatientName.ToString().ToUpper().Contains(searchBy.ToUpper())  ||
                r.Doctor != null && r.Doctor.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }

        public Record GetDetails(int id)
        {
            Record result = _recordService.GetAll().Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

    }
}
