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
    public class PatientsRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly string baseQuery;
        public PatientsRepository(string conn) : base (conn)
        {
            baseQuery = $"SELECT p.{nameof(Patient.Id)}, " +
              $"{nameof(Patient.UserId)}, " +
              $"{nameof(Patient.Job)}, " +
              $"up.{nameof(User.Login)} as {nameof(Patient)}{nameof(User.Login)}, " +
              $"up.{nameof(User.Password)} as {nameof(Patient)}{nameof(User.Password)}, " +
              $"up.{nameof(User.FirstName)} as {nameof(Patient)}{nameof(User.FirstName)}, " +
              $"up.{nameof(User.LastName)} as {nameof(Patient)}{nameof(User.LastName)}, " +
              $"up.{nameof(User.RoleId)} as {nameof(Patient)}{nameof(User.RoleId)}, " +
              $"up.{nameof(User.Birthday)} as {nameof(Patient)}{nameof(User.Birthday)}, " +
              $"up.{nameof(User.Email)} as {nameof(Patient)}{nameof(User.Email)}, " +
              $"up.{nameof(User.PhoneNumber)} as {nameof(Patient)}{nameof(User.PhoneNumber)}, " +
              $"up.{nameof(User.PhotoUrl)} as {nameof(Patient)}{nameof(User.PhotoUrl)} " +
              $"FROM {nameof(Patient).ConvertToTableName()} as p " +
              $"LEFT JOIN {nameof(User).ConvertToTableName()} as up ON up.{nameof(User.Id)} = p.{nameof(Patient.UserId)}";
        }

        public override IEnumerable<Patient> GetAll()
        {
            return ExecuteReader(baseQuery);
        }

        public override IEnumerable<Patient> GetByColumn(string column, string value)
        {
            string sqlExpression = $"{baseQuery} WHERE [{column}] = @value";
            return ExecuteReader(sqlExpression, new SqlParameter("@value", value));
        }

        public IEnumerable<Patient> GetByDoctorId(int doctorId)
        {
            string sqlExpression = $"{baseQuery} WHERE p.[Id] IN (SELECT PatientId from tRecord where DoctorId = @doctorId)";
            return ExecuteReader(sqlExpression, new SqlParameter("@doctorId", doctorId));
        }

        public override Patient GetById(int id)
        {
            string sqlExpression = $"{baseQuery} WHERE p.Id = @id";
            return ExecuteReader(sqlExpression, new SqlParameter("@id", id)).FirstOrDefault();
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Patient).ConvertToTableName()}](
                                                         [{nameof(Patient.UserId)}]
                                                        ,[{nameof(Patient.Job)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 21; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}" +
                    $"('{i}'" +
                    $",'{Gen.Random.Text.Short()()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override Patient Map(SqlDataReader reader)
        {
            var model = new Patient();

            model.Id = (int)reader[nameof(model.Id)];
            model.UserId = (int)reader[nameof(model.UserId)];
            model.Job = (string)reader[nameof(model.Job)];
            model.User.Login = (string)reader[$"{nameof(Patient)}{nameof(model.User.Login)}"];
            model.User.Password = (string)reader[$"{nameof(Patient)}{nameof(model.User.Password)}"];
            model.User.FirstName = (string)reader[$"{nameof(Patient)}{nameof(model.User.FirstName)}"];
            model.User.LastName = (string)reader[$"{nameof(Patient)}{nameof(model.User.LastName)}"];
            model.User.RoleId = (int)reader[$"{nameof(Patient)}{nameof(model.User.RoleId)}"];
            model.User.Birthday = (DateTime)reader[$"{nameof(Patient)}{nameof(model.User.Birthday)}"];
            model.User.Email = (string)reader[$"{nameof(Patient)}{nameof(model.User.Email)}"];
            model.User.PhoneNumber = (string)reader[$"{nameof(Patient)}{nameof(model.User.PhoneNumber)}"];
            model.User.PhotoUrl = (string)reader[$"{nameof(Patient)}{nameof(model.User.PhotoUrl)}"];

            return model;
        }
    }
}
