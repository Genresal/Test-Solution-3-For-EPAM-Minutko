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
        public IEnumerable<RecordViewModel> LoadTable(RecordSearchModel searchParameters);
        public IEnumerable<Doctor> GetDoctors();
        public IEnumerable<Patient> GetPatients();
        public Patient GetPatient(int id);
        public IEnumerable<Position> GetDoctorPositions();

        public RecordDetailsViewModel Details(int id);
    }
}
