using EMR.Data.Helpers;
using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;
using EMR.Business.Repositories;

namespace EMR.Data.Repositories
{
    public class PatientInfoRepository : BaseRepository<PatientInfo>, IPatientInfoRepository
    {
        public PatientInfoRepository(string conn) : base (conn)
        {
        }
            public IEnumerable<PatientInfo> GetPatientsInfo(int doctorId)
            {
            return StoredExecuteReader("GetPatientsInfo", new SqlParameter("@doctorId", doctorId));
        }

        protected override PatientInfo Map(SqlDataReader reader)
        {
            var model = new PatientInfo();

            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.RecordsNumber = (int)reader[nameof(model.RecordsNumber)];
            model.LastRecordModified = (DateTime)reader[nameof(model.LastRecordModified)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];

            return model;
        }
    }
}
