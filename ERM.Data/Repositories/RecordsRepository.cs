using EMR.Data.Helpers;
using EMR.Business.Models;
using RandomGen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMR.Data.Repositories
{
    public class RecordsRepository : BaseRepository<Record>
    {
        private readonly string baseQuery;
        public RecordsRepository(string conn) : base (conn)
        {
           baseQuery = $"SELECT r.{nameof(Record.Id)}, " +
                         $"{nameof(Record.DiagnosisId)}, " +
                         $"{nameof(Record.SickLeaveId)}, " +
                         $"{nameof(Record.DoctorId)}, " +
                         $"{nameof(Record.PatientId)}, " +
                         $"{nameof(Record.ModifiedDate)}, " +
                         $"di.{nameof(Diagnosis.Name)} as {nameof(Diagnosis)}{nameof(Diagnosis.Name)}, " +
                         $"d.{nameof(Doctor.PositionId)} as {nameof(Doctor)}{nameof(Doctor.PositionId)}, " +
                         $"d.{nameof(Doctor.UserId)} as {nameof(Doctor)}{nameof(Doctor.UserId)}, " +
                         $"p.{nameof(Patient.Job)} as {nameof(Patient)}{nameof(Patient.Job)}, " +
                         $"p.{nameof(Patient.UserId)} as {nameof(Patient)}{nameof(Patient.UserId)}, " +
                         $"ud.{nameof(User.FirstName)} as {nameof(Doctor)}{nameof(User.FirstName)}, " +
                         $"ud.{nameof(User.LastName)} as {nameof(Doctor)}{nameof(User.LastName)}, " +
                         $"ud.{nameof(User.Birthday)} as {nameof(Doctor)}{nameof(User.Birthday)}, " +
                         $"ud.{nameof(User.PhoneNumber)} as {nameof(Doctor)}{nameof(User.PhoneNumber)}, " +
                         $"up.{nameof(User.FirstName)} as {nameof(Patient)}{nameof(User.FirstName)}, " +
                         $"up.{nameof(User.LastName)} as {nameof(Patient)}{nameof(User.LastName)}, " +
                         $"up.{nameof(User.Birthday)} as {nameof(Patient)}{nameof(User.Birthday)}, " +
                         $"up.{nameof(User.PhoneNumber)} as {nameof(Patient)}{nameof(User.PhoneNumber)}, " +
                         $"pos.{nameof(Position.Name)} as {nameof(Doctor)}{nameof(Position.Name)}, " +
                         $"s.{nameof(SickLeave.StartDate)} as {nameof(SickLeave)}{nameof(SickLeave.StartDate)}, " +
                         $"s.{nameof(SickLeave.FinalDate)} as {nameof(SickLeave)}{nameof(SickLeave.FinalDate)} " +
                         $"FROM {nameof(Record).ConvertToTableName()} as r " +
                         $"LEFT JOIN {nameof(Doctor).ConvertToTableName()} as d ON d.{nameof(Doctor.Id)} = r.{nameof(Record.DoctorId)} " +
                         $"LEFT JOIN {nameof(Patient).ConvertToTableName()} as p ON p.{nameof(Patient.Id)} = r.{nameof(Record.PatientId)} " +
                         $"LEFT JOIN {nameof(User).ConvertToTableName()} as ud ON ud.{nameof(User.Id)} = d.{nameof(Doctor.UserId)} " +
                         $"LEFT JOIN {nameof(User).ConvertToTableName()} as up ON up.{nameof(User.Id)} = p.{nameof(Patient.UserId)} " +
                         $"LEFT JOIN {nameof(Diagnosis).ConvertToTableName()} as di ON di.{nameof(Diagnosis.Id)} = r.{nameof(Record.DiagnosisId)} " +
                         $"LEFT JOIN {nameof(Position).ConvertToTableName()} as pos ON pos.{nameof(Position.Id)} = d.{nameof(Doctor.PositionId)} " +
                         $"LEFT JOIN {nameof(SickLeave).ConvertToTableName()} as s ON s.{nameof(SickLeave.Id)} = r.{nameof(SickLeave.Id)}";
        }
        
        public override IEnumerable<Record> GetAll()
        {
            List<Record> items = new List<Record>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(baseQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(Map(reader));
                    }
                }
                connection.Close();
            }
            return items;
        }

        public override Record GetById(int id)
        {
            Record result = default(Record);

            string sqlExpression = $"{baseQuery} " +
                                   $"WHERE r.[Id] = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = (Map(reader));
                    }
                }

                connection.Close();
            }
            return result;
        }

        public override void Delete(int id)
        {

            string sqlExpression = $"BEGIN TRY " +
                $"BEGIN TRAN " +
                $"DECLARE @Id int = @parId " +
                $"DELETE FROM [dbo].[{nameof(RecordTreatment).ConvertToTableName()}] WHERE {nameof(RecordTreatment.RecordId)} = @Id " +
                $"DELETE FROM {typeof(Record).Name.ConvertToTableName()} WHERE Id = @Id " +
                $"COMMIT TRAN " +
                $"END TRY " +
                $"BEGIN CATCH " +
                $"ROLLBACK TRAN " +
                $"END CATCH";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@parId", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public override void SetDefaultData()
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
                sqlExpression = $"{sqlExpression}('{Gen.Random.Numbers.Integers(1, 100)()}'" +
                                $",'{i}'" +
                                $",'{Gen.Random.Numbers.Integers(1, 20)()}'" +
                                $",'{Gen.Random.Numbers.Integers(1, 80)()}'" +
                                $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-1), DateTime.Now)()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }

            SetDefaultData(sqlExpression);
        }
    

    protected override Record Map(SqlDataReader reader)
        {
            var model = new Record();

            model.Id = (int)reader[nameof(model.Id)];
            model.DiagnosisId = (int)reader[nameof(model.DiagnosisId)];
            model.SickLeaveId = (int)reader[nameof(model.SickLeaveId)];
            model.DoctorId = (int)reader[nameof(model.DoctorId)];
            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.ModifiedDate = (DateTime)reader[nameof(model.ModifiedDate)];
            model.Diagnosis.Name = (string)reader[$"{nameof(Diagnosis)}{nameof(Diagnosis.Name)}"];
            model.Doctor.PositionId = (int)reader[$"{nameof(Doctor)}{nameof(model.Doctor.PositionId)}"];
            model.Doctor.Position.Name = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.Position.Name)}"];
            model.Doctor.UserId = (int)reader[$"{nameof(Doctor)}{nameof(model.Doctor.UserId)}"];
            model.Patient.Job = (string)reader[$"{nameof(Patient)}{nameof(model.Patient.Job)}"];
            model.Patient.UserId = (int)reader[$"{nameof(Patient)}{nameof(model.Patient.UserId)}"];
            model.Doctor.User.FirstName = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.FirstName)}"];
            model.Doctor.User.LastName = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.LastName)}"];
            model.Doctor.User.Birthday = (DateTime)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.Birthday)}"];
            model.Doctor.User.PhoneNumber = (string)reader[$"{nameof(Doctor)}{nameof(model.Doctor.User.PhoneNumber)}"];
            model.Patient.User.FirstName = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.FirstName)}"];
            model.Patient.User.LastName = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.LastName)}"];
            model.Patient.User.Birthday = (DateTime)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.Birthday)}"];
            model.Patient.User.PhoneNumber = (string)reader[$"{nameof(Patient)}{nameof(model.Doctor.User.PhoneNumber)}"];
            model.SickLeave.Id = model.SickLeaveId;
            model.SickLeave.StartDate = (DateTime)reader[$"{nameof(SickLeave)}{nameof(SickLeave.StartDate)}"];
            model.SickLeave.FinalDate = (DateTime)reader[$"{nameof(SickLeave)}{nameof(SickLeave.FinalDate)}"];

            return model;
        }
    }
}
