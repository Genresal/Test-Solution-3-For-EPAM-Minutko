using AutoMapper;
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
        private readonly IBusinessService<Record> _recordService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IBusinessService<Position> _positionService;
        private readonly ITreatmentService _treatmentService;
        private readonly IMapper _mapper;

        public RecordPageService(IBusinessService<Record> sr
            , IDoctorService d
            , IPatientService p
            , IBusinessService<Position> sp
            , ITreatmentService t
            , IMapper mapper)
        {
            _recordService = sr;
            _doctorService = d;
            _patientService = p;
            _positionService = sp;
            _treatmentService = t;
            _mapper = mapper;
        }

        public IEnumerable<Position> GetDoctorPositions()
        {
            return _positionService.GetAll();
        }

        public IEnumerable<RecordViewModel> LoadTable(RecordSearchModel searchParameters)
        {
            IEnumerable<Record> rawResult;

            if (searchParameters.PatientId > 0)
            {
                rawResult = _recordService.GetByColumn(nameof(searchParameters.PatientId), searchParameters.PatientId.ToString());
            }
            else if (searchParameters.DoctorId > 0)
            {
                rawResult = _recordService.GetByColumn(nameof(searchParameters.DoctorId), searchParameters.DoctorId.ToString());
            }
            else
            {
                rawResult = _recordService.GetAll();
            }

            var result = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordViewModel>>(rawResult);

            if (searchParameters.DoctorPositions.Count > 0)
            {
                var filterParams = searchParameters.DoctorPositions.Select(r => r.Id);
                result = result.Where(r => filterParams.Contains(r.DoctorPositionId));
            }

            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            if (!string.IsNullOrEmpty(searchParameters.DateRange.Start))
            {
                startDate = DateTime.Parse(searchParameters.DateRange.Start);
            }

            if (!string.IsNullOrEmpty(searchParameters.DateRange.End))
            {
                endDate = DateTime.Parse(searchParameters.DateRange.End);
            }

            if (startDate != DateTime.MinValue)
            {
                result = result.Where(u => u.ModifiedDate >= startDate);
            }

            if (endDate != DateTime.MinValue)
            {
                result = result.Where(u => u.ModifiedDate <= endDate);
            }

            if (!string.IsNullOrEmpty(searchParameters.Diagnosis))
            {
                result = result.Where(r => r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchParameters.Diagnosis.ToUpper()));
            }

            var searchBy = searchParameters.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.PatientName != null && r.PatientName.ToString().ToUpper().Contains(searchBy.ToUpper())  ||
                r.DoctorName != null && r.DoctorName.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                r.Diagnosis != null && r.Diagnosis.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }

        public RecordDetailsViewModel Details(int id)
        {
            var rawResult = _recordService.GetById(id);
            var result = _mapper.Map<Record, RecordDetailsViewModel>(rawResult);

            return result;
        }

        public Record GetById(int id)
        {
            return _recordService.GetById(id);
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

        public IEnumerable<Doctor> GetDoctors()
        {
            var result = _doctorService.GetAll();
            return result.ToList();
        }

        public IEnumerable<Patient> GetPatients()
        {
            var result = _patientService.GetAll();
            return result.ToList();
        }

        public Patient GetPatient(int id)
        {
            var result = _patientService.GetById(id);
            return result;
        }

        public IEnumerable<Drug> LoadDrugTable(DrugSearchModel searchParameters)
        {
            var result = _treatmentService.GetAllDrugs(searchParameters.RecordId);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            result = Order(searchParameters, result);

            return result;
        }

        public IEnumerable<Procedure> LoadProcedureTable(ProcedureSearchModel searchParameters)
        {
            var result = _treatmentService.GetAllProcedures(searchParameters.RecordId);

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
