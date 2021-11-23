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

        public DbService(IRepository<SickLeave> sickLeave
            , IRepository<Diagnosis> diagnosis
            , IRepository<Drug> drug
            , IRepository<Procedure> procedure
            , IRepository<Role> role
            , IRepository<User> user
            , IRepository<Position> position
            , IRepository<Doctor> doctor
            , IRepository<Patient> patient
            , IRepository<Record> record
            , IRepository<RecordTreatment> recordTreatment)
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

        }

        public void CheckDb()
        {
            foreach(var repo in repositories)
            {
                repo.CheckTable();
            }
        }
    }
}
