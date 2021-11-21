using ERM.DataTables;
using ERM.Models;
using ERM.Repositories;
using ERM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.Services
{
    public class RecordsService : BaseService, IService
    {
        RecordsRepository _recordsRepository;
        IRepository<Doctor> _doctorsRepository;
        IRepository<Patient> _patientsRepository;
        IRepository<SickLeave> _sickLeavesRepository;
        IRepository<Treatment> _treatmentsRepository;

        public RecordsService(RecordsRepository r
                        , IRepository<Doctor> rr
                        , IRepository<Patient> rrr
                        , IRepository<SickLeave> rrrr
                        , IRepository<Treatment> rrrrr)
        {
            _recordsRepository = r;
            _doctorsRepository = rr;
            _patientsRepository = rrr;
            _sickLeavesRepository = rrrr;
            _treatmentsRepository = rrrrr;
        }
        public IQueryable<RecordViewModel>LoadTable(RecordSearchModel searchParameters)
        {
            var result = _recordsRepository.GetAllViewModel().AsQueryable();

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
                result = result.Where(u => u.ModifyingDate >= date1);

            if (date2 != DateTime.MinValue)
                result = result.Where(u => u.ModifyingDate <= date2);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }
    }
}
