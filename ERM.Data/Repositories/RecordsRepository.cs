using EMR.Data.Helpers;
using EMR.Business.Models;
using RandomGen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EMR.Business.Repositories;
using System.Data;

namespace EMR.Data.Repositories
{
    public class RecordsRepository : BaseRepository<Record>, IRepository<Record>
    {
        public RecordsRepository(string conn) : base (conn)
        {
        }
        
        public override IEnumerable<Record> GetAll()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", DBNull.Value));
            parameters.Add(new SqlParameter("@OPERATOR", DBNull.Value));
            parameters.Add(new SqlParameter("@VALUE", DBNull.Value));
            return StoredExecuteReader("GetRecords", parameters);
        }

        public override IEnumerable<Record> GetByColumn(string column, string value)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", column));
            parameters.Add(new SqlParameter("@OPERATOR", "="));
            parameters.Add(new SqlParameter("@VALUE", value));
            return StoredExecuteReader("GetRecords", parameters);
        }

        public override Record GetById(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", "rec.Id"));
            parameters.Add(new SqlParameter("@OPERATOR", "="));
            parameters.Add(new SqlParameter("@VALUE", id));
            return StoredExecuteReader("GetRecords", parameters).FirstOrDefault();
        }

        public override void Create(Record model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DiagnosisName", model.Diagnosis.Name));
            parameters.Add(new SqlParameter("DoctorId", model.DoctorId));
            parameters.Add(new SqlParameter("PatientId", model.PatientId));
            parameters.Add(new SqlParameter("ModifiedDate", model.ModifiedDate));

            StoredExecuteNonQuery("CreateRecord", parameters);
        }

        public override void Delete(int id)
        {
            StoredExecuteNonQuery("DeleteRecord", new SqlParameter("@Id", id));
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Record).ConvertToTableName()}](
                                                         [{nameof(Record.DiagnosisId)}]
                                                        ,[{nameof(Record.SickLeaveId)}]
                                                        ,[{nameof(Record.DoctorId)}]
                                                        ,[{nameof(Record.PatientId)}]
                                                        ,[{nameof(Record.ModifiedDate)}])
                                                    VALUES";
            int dataCount = 200;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}(" +
                                $"'{i}'" +
                                $",'{i}'" +
                                $",'{Gen.Random.Numbers.Integers(1, 20)()}'" +
                                $",'{Gen.Random.Numbers.Integers(1, 80)()}'" +
                                $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-1), DateTime.Now)()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }

            ExecuteNonQuery(sqlExpression);
        }
    

    protected override Record Map(SqlDataReader reader)
        {
            var model = new Record();

            model.Id = (int)reader[nameof(model.Id)];
            model.DiagnosisId = (int)reader[nameof(model.DiagnosisId)];
            model.SickLeaveId = Convert.IsDBNull(reader[nameof(model.SickLeaveId)]) ? null : (int?)reader[nameof(model.SickLeaveId)];
            model.DoctorId = (int)reader[nameof(model.DoctorId)];
            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.ModifiedDate = (DateTime)reader[nameof(model.ModifiedDate)];
            model.Doctor.Id = model.DoctorId;
            model.Diagnosis.Name = (string)reader[$"{nameof(Diagnosis)}{nameof(Diagnosis.Name)}"];
            model.Doctor.PositionId = (int)reader[$"{nameof(Doctor)}{nameof(model.Doctor.PositionId)}"];
            model.Doctor.Position.Name = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.Position.Name)}"];
            model.Doctor.UserId = (int)reader[$"{nameof(Doctor)}{nameof(model.Doctor.UserId)}"];
            model.Patient.Id = model.PatientId;
            model.Patient.Job = (string)reader[$"{nameof(Patient)}{nameof(model.Patient.Job)}"];
            model.Patient.UserId = (int)reader[$"{nameof(Patient)}{nameof(model.Patient.UserId)}"];
            model.Doctor.User.FirstName = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.FirstName)}"];
            model.Doctor.User.LastName = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.LastName)}"];
            model.Doctor.User.Birthday = (DateTime)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.Birthday)}"];
            model.Doctor.User.Email = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.Email)}"];
            model.Doctor.User.PhoneNumber = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.PhoneNumber)}"];
            model.Patient.User.FirstName = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.FirstName)}"];
            model.Patient.User.LastName = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.LastName)}"];
            model.Patient.User.Birthday = (DateTime)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.Birthday)}"];
            model.Patient.User.Email = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.Email)}"];
            model.Patient.User.PhoneNumber = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.PhoneNumber)}"];
            if (!Convert.IsDBNull(reader[$"{nameof(SickLeave)}{nameof(SickLeave.Id)}"]))
            {
                model.SickLeave.Id = (int)reader[$"{nameof(SickLeave)}{nameof(SickLeave.Id)}"];
                model.SickLeave.StartDate = (DateTime)reader[$"{nameof(SickLeave)}{nameof(SickLeave.StartDate)}"];
                model.SickLeave.FinalDate = (DateTime)reader[$"{nameof(SickLeave)}{nameof(SickLeave.FinalDate)}"];
            }

            return model;
        }
    }
}
