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
                string sqlExpression = $"SELECT [PatientId], " +
                    $"COUNT(t.Id) as {nameof(PatientInfo.RecordsNumber)}, " +
                    $"MAX([ModifiedDate]) as {nameof(PatientInfo.LastRecordModified)}, " +
                    $"(SELECT CONCAT([FirstName], ' ', [LastName]) FROM [tUser] WHERE [Id] = MAX(p.UserId)) as {nameof(PatientInfo.FullName)} " +
                    $"FROM [tRecord] as t " +
                    $"LEFT JOIN [tPatient] as p on p.Id = t.PatientId " +
                    $"WHERE [DoctorId] = @doctorId " +
                    $"GROUP BY [PatientId]";

            return ExecuteReader(sqlExpression, new SqlParameter("@doctorId", doctorId));
        }

        protected override PatientInfo Map(SqlDataReader reader)
        {
            var model = new PatientInfo();

            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.RecordsNumber = (int)reader[nameof(model.RecordsNumber)];
            model.LastRecordModified = (DateTime)reader[nameof(model.LastRecordModified)];
            model.FullName = (string)reader[nameof(model.FullName)];

            return model;
        }
    }
}
