using EMR.Business.Models;
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
        public IQueryable<Drug> LoadDrugTable(DrugSearchModel searchParameters);
        public IQueryable<Procedure> LoadProcedureTable(ProcedureSearchModel searchParameters);
        public List<Doctor> GetDoctors();
        public List<Patient> GetPatients();
        public void Create(Record item);
        public void Update(Record item);
        public void Delete(int id);
        public Record GetById(int id);
        public IEnumerable<Position> GetDoctorPositions();
    }
}
