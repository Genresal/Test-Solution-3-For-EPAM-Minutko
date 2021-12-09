using EMR.Business.Models;
using EMR.Business.Repositories;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public class DbService : IDbService
    {
        private List<IRepository> repositories = new List<IRepository>();

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

        public DbStatus GetDbStatus()
        {
            DbStatus result = new DbStatus();

            result.IsTablesExist = true;

            foreach (var repo in repositories)
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
            foreach (var repo in repositories)
            {
                if (!repo.IsTableHasRecords())
                {
                    repo.SetDefaultData();
                }
            }
        }
        public void DropTables()
        {
            for (int i = repositories.Count - 1; i >= 0; i--)
            {
                repositories[i].DropTable();
            }
        }
    }
}
