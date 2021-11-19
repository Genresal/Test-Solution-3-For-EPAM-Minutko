using ERM.Helpers;
using ERM.Models;
using ERM.ViewModels;
using RandomGen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERM.Repositories
{
    public class RecordsRepository : BaseRepository<Record>
    {
        public RecordsRepository(string conn) : base (conn)
        {
        }
        /*
        public IEnumerable<Record> GetAll()
        {
            List<Record> Records = new List<Record>();


            string sqlExpression = "SELECT * FROM Records";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Record record = new Record();
                        Map(reader, record);
                        Records.Add(record);
                    }
                }
                connection.Close();
            }
            return Records;
        }

        public Record FindById(int id)
        {
            Record record = new Record();
            string sqlExpression = @"SELECT * FROM Records" +
                                     " WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                        Map(reader, record);
                }
                connection.Close();
            }
            return record;
        }

        public void Create(Record record)
        {
            string sqlExpression = @"INSERT INTO Records ([PatientId]" +
                                                        ",[Diagnosis]" +
                                                        ",[DoctorId]" +
                                                        ",[SickLeaveId]" +
                                                        ",[ModifyDate])" +
                                                 "VALUES (@PatientId" +
                                                        ",@Diagnosis" +
                                                        ",@DoctorId" +
                                                        ",@SickLeaveId" +
                                                        ",@ModifyDate)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@PatientId", record.PatientId));
                command.Parameters.Add(new SqlParameter("@Diagnosis", record.Diagnosis));
                command.Parameters.Add(new SqlParameter("@DoctorId", record.DoctorId));
                command.Parameters.Add(new SqlParameter("@SickLeaveId", record.SickLeaveId));
                command.Parameters.Add(new SqlParameter("@ModifyDate", record.ModifyDate));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(Record record)
        {
            string sqlExpression = @"UPDATE Records" +
                                    " SET[PatientId] = @PatientId" +
                                       ",[Diagnosis] = @Diagnosis" +
                                       ",[DoctorId] = @DoctorId" +
                                       ",[SickLeaveId] = @SickLeaveId" +
                                       ",[ModifyDate] = @ModifyDate" +
                                   " WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", record.Id));
                command.Parameters.Add(new SqlParameter("@PatientId", record.PatientId));
                command.Parameters.Add(new SqlParameter("@Diagnosis", record.Diagnosis));
                command.Parameters.Add(new SqlParameter("@DoctorId", record.DoctorId));
                command.Parameters.Add(new SqlParameter("@SickLeaveId", record.SickLeaveId));
                command.Parameters.Add(new SqlParameter("@ModifyDate", record.ModifyDate));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        
        public void Delete(int id)
        {
            string sqlExpression = @"DELETE FROM Records" +
                                   " WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        */
        public IEnumerable<RecordViewModel> GetAllViewModel()
        {
            List<RecordViewModel> items = new List<RecordViewModel>();

            string sqlExpression = $"SELECT r.{nameof(Record.Id)}, " +
                                    $"{nameof(Record.PatientId)}, " +
                                    $"{nameof(Record.Diagnosis)}, " +
                                    $"{nameof(Record.DoctorId)}, " +
                                    $"{nameof(Record.ModifyingDate)}, " +
                                    $"CONCAT('Dr. ', d.{nameof(Doctor.FirstName)}, ' ',d.{nameof(Doctor.LastName)})  as {nameof(RecordViewModel.DoctorName)}, " +
                                    $"CONCAT(p.{nameof(Patient.FirstName)}, ' ',p.{nameof(Patient.LastName)})  as {nameof(RecordViewModel.PatientName)}, " +
                                    $"{nameof(Record.ModifyingDate)} " +
                                    $"FROM {nameof(Record).MakePlular()} as r  " +
                                    $"LEFT JOIN {nameof(Doctor).MakePlular()} as d ON d.{nameof(Doctor.Id)} = r.{nameof(Record.DoctorId)} " +
                                    $"LEFT JOIN {nameof(Patient).MakePlular()} as p ON p.{nameof(Patient.Id)} = r.{nameof(Record.PatientId)}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(MapViewModel(reader));
                    }
                }
                connection.Close();
            }
            return items;
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Record).MakePlular()}](
                                                         [{nameof(Record.PatientId)}]
                                                        ,[{nameof(Record.Diagnosis)}]
                                                        ,[{nameof(Record.DoctorId)}]
                                                        ,[{nameof(Record.ModifyingDate)}])
                                                    VALUES";
            int dataCount = 200;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression += "('" + Gen.Random.Numbers.Integers(1, 100)() +
                    "','" + Gen.Random.Text.Short()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.Numbers.Integers(1, 20)() +
                    "','" + Gen.Random.Time.Dates(DateTime.Now.AddYears(-1), DateTime.Now)() +
                    "')";
                if (i != dataCount)
                {
                    sqlExpression += ",";
                }

            }

            SetDefaultData(sqlExpression);
        }
    

    protected override Record Map(SqlDataReader reader)
        {
            var model = new Record();

            model.Id = (int)reader[nameof(model.Id)];
            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.Diagnosis = (string)reader[nameof(model.Diagnosis)];
            model.DoctorId = (int)reader[nameof(model.DoctorId)];
            model.ModifyingDate = (DateTime)reader[nameof(model.ModifyingDate)];

            return model;
        }

        protected RecordViewModel MapViewModel(SqlDataReader reader)
        {
            var model = new RecordViewModel();

            model.Id = (int)reader[nameof(model.Id)];
            model.PatientId = (int)reader[nameof(model.PatientId)];
            model.Diagnosis = (string)reader[nameof(model.Diagnosis)];
            model.DoctorId = (int)reader[nameof(model.DoctorId)];
            model.ModifyingDate = (DateTime)reader[nameof(model.ModifyingDate)];
            model.DoctorName = (string)reader[nameof(model.DoctorName)];
            model.PatientName = (string)reader[nameof(model.PatientName)];

            return model;
        }
    }
}
