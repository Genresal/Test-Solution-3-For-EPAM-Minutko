using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IRecordPageService : IBasePageService<Record>
    {
        public IQueryable<RecordViewModel> LoadTable(RecordSearchModel searchParameters);
        public IQueryable<Drug> LoadDrugTable(DrugSearchModel searchParameters);
        public IQueryable<Procedure> LoadProcedureTable(ProcedureSearchModel searchParameters);
        public List<Doctor> GetDoctors();
        public List<Patient> GetPatients();
        public Patient GetPatient(int id);
        public IEnumerable<Position> GetDoctorPositions();
        public Record GetById(int id);
    }
}
