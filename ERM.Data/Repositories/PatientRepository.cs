using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using RandomGen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMR.Data.Repositories
{
    public class PatientsRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientsRepository(string conn) : base(conn)
        {
        }

        public override IEnumerable<Patient> GetAll()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", DBNull.Value));
            parameters.Add(new SqlParameter("@OPERATOR", DBNull.Value));
            parameters.Add(new SqlParameter("@VALUE", DBNull.Value));
            return StoredExecuteReader("GetPatients", parameters);
        }

        public override IEnumerable<Patient> GetByColumn(string column, string value)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", column));
            parameters.Add(new SqlParameter("@OPERATOR", "="));
            parameters.Add(new SqlParameter("@VALUE", value));
            return StoredExecuteReader("GetPatients", parameters);
        }

        public override Patient GetById(int id)
        {
            return GetByColumn("pat.Id", id).FirstOrDefault();
        }

        public IEnumerable<Patient> GetByDoctorId(int doctorId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", "pat.Id"));
            parameters.Add(new SqlParameter("@OPERATOR", "IN"));
            parameters.Add(new SqlParameter("@VALUE", "(SELECT PatientId from tRecord where DoctorId = @doctorId)"));
            return StoredExecuteReader("GetPatients", parameters);
        }

        public override void Create(Patient model)
        {
            var userProperties = model.User.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .Where(x => x.Name != "Id")
                        .ToList();

            var parameters = ProrertiesToSqlParameters(model.User, userProperties);
            parameters.Add(new SqlParameter(nameof(model.Job), model.Job));

            StoredExecuteNonQuery("CreatePatient", parameters);
        }

        public override void Update(Patient model)
        {
            var patientProperties = model.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .ToList();

            var userProperties = model.User.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .Where(x => x.Name != "Id")
                        .ToList();

            var parameters = ProrertiesToSqlParameters(model, patientProperties);
            parameters.AddRange(ProrertiesToSqlParameters(model.User, userProperties));

            StoredExecuteNonQuery("UpdatePatient", parameters);
        }

        public override void Delete(int id)
        {
            StoredExecuteNonQuery("DeletePatient", new SqlParameter("@Id", id));
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
            model.User.PhotoUrl = Convert.IsDBNull(reader[$"{nameof(Patient)}{nameof(model.User.PhotoUrl)}"]) ? null : (string)reader[$"{nameof(Patient)}{nameof(model.User.PhotoUrl)}"];

            return model;
        }
    }
}
