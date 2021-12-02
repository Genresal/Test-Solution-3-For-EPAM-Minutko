using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Business.Services
{
    public class DbService : IDbService
    {
        /*
        IRepository<SickLeave> _sickLeave;
        IRepository<Diagnosis> _diagnosis;
        IRepository<Drug> _drug;
        IRepository<Procedure> _procedure;
        IRepository<Role> _role;
        IRepository<User> _user;
        IRepository<Position> _position;
        IRepository<Doctor> _doctor;
        IRepository<Patient> _patient;
        IRepository<Record> _record;
        IRepository<RecordTreatment> _recordTreatment;
        */
        private List<IRepository> repositories = new List<IRepository>();
        private IDbRepository _dbRepository;

        public DbService(IRepository<SickLeave> sickLeave
            , IRepository<Diagnosis> diagnosis
            , IRepository<Drug> drug
            , IRepository<Procedure> procedure
            , IRepository<Role> role
            , IRepository<User> user
            , IRepository<Position> position
            , IRepository<Doctor> doctor
            , IPatientRepository patient
            , IRepository<Record> record
            , IRepository<RecordTreatment> recordTreatment
            , IDbRepository dbRepository)
        {
            repositories.Add(sickLeave);
            repositories.Add(diagnosis);
            repositories.Add(drug);
            repositories.Add(procedure);
            repositories.Add(role);
            repositories.Add(user);
            repositories.Add(position);
            repositories.Add(doctor);
            repositories.Add(patient);
            repositories.Add(record);
            repositories.Add(recordTreatment);
            _dbRepository = dbRepository;
        }

        public DbStatus GetDbStatus()
        {
            DbStatus result = new DbStatus();
            result.IsDbExist = _dbRepository.IsDbExist();

            if(!result.IsDbExist)
            {
                return result;
            }

            result.IsTablesExist = true;

            foreach(var repo in repositories)
            {
                if (!repo.IsTableExist())
                {
                    result.IsTablesExist = false;
                    break;
                }
            }

            if (!result.IsTablesExist)
            {
                return result;
            }

            result.IsDataExist = true;
            foreach (var repo in repositories)
            {
                if (!repo.IsTableHasRecords())
                {
                    result.IsDataExist = false;
                    break;
                }
            }

            return result;
        }

        public void CreateDefaultDate()
        {
            foreach(var repo in repositories)
            {
                repo.CreateDefaultDate();
            }
        }
        public void CreateDb()
        {
            _dbRepository.CreateDb();
        }
        public void DropDb()
        {
            _dbRepository.DropDb();
        }
        public void CreateTables()
        {
            _dbRepository.CreateTables();
        }
        public void DropTables()
        {
            for (int i = repositories.Count-1; i >= 0; i--)
            {
                repositories[i].DropTable();
            }
        }
    }
}
