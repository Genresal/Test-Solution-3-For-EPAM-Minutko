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
                sqlExpression = $"{sqlExpression}('{Gen.Random.Numbers.Integers(1, 100)()}'" +
                                $",'{Gen.Random.Text.Short()().MakeFirstCharUppercase()}'" +
                                $",'{Gen.Random.Numbers.Integers(1, 20)()}'" +
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
