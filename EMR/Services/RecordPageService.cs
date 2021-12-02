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
        IDoctorService _doctorService;
        IPatientService _patientService;
        IBusinessService<Position> _positionService;
        ITreatmentService _treatmentService;

        public RecordPageService(IBusinessService<Record> sr
            , IDoctorService d
            , IPatientService p
            , IBusinessService<Position> sp
            , ITreatmentService t)
        {
            _recordService = sr;
            _doctorService = d;
            _patientService = p;
            _positionService = sp;
            _treatmentService = t;
        }

        public IEnumerable<Position> GetDoctorPositions()
        {
            return _positionService.GetAll();
        }

        public IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters)
        {
            IQueryable<RecordViewModel> result;
            if (searchParameters.PatientId > 0)
            {
                result = _recordService.GetByColumn(nameof(searchParameters.PatientId), searchParameters.PatientId.ToString())
                                        .Select(x => x.ToViewModel())
                                        .AsQueryable();
            }
            else if (searchParameters.DoctorId > 0)
            {
                result = _recordService.GetByColumn(nameof(searchParameters.DoctorId), searchParameters.DoctorId.ToString())
                                        .Select(x => x.ToViewModel())
                                        .AsQueryable();
            }
            else
            {
                result = _recordService.GetAll()
                                        .Select(x => x.ToViewModel())
                                        .AsQueryable();
            }

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

        public Record GetById(int id)
        {
            Record result = _recordService.GetById(id);
            return result;
        }

        public void Create(Record item)
        {
            _recordService.Create(item);
        }
        public void Update(Record item)
        {
            _recordService.Update(item);
        }
        public void Delete(int id)
        {
            _recordService.Delete(id);
        }

        public List<Doctor> GetDoctors()
        {
            var result = _doctorService.GetAll();
            return result.ToList();
        }

        public List<Patient> GetPatients()
        {
            var result = _patientService.GetAll();
            return result.ToList();
        }

        public Patient GetPatient(int id)
        {
            var result = _patientService.GetById(id);
            return result;
        }

        public IQueryable<Drug> LoadDrugTable(DrugSearchModel searchParameters)
        {
            var result = _treatmentService.GetAllDrugs(searchParameters.RecordId).AsQueryable();

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }

        public IQueryable<Procedure> LoadProcedureTable(ProcedureSearchModel searchParameters)
        {
            var result = _treatmentService.GetAllProcedures(searchParameters.RecordId).AsQueryable();

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }
    }
}
