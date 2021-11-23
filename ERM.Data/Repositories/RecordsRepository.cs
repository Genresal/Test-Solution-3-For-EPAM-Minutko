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
        public RecordsRepository(string conn) : base (conn)
        {
        }
        
        public override IEnumerable<Record> GetAll()
        {
            List<Record> items = new List<Record>();

            string sqlExpression = $"SELECT r.{nameof(Record.Id)}, " +
                                    $"{nameof(Record.DiagnosisId)}, " +
                                    $"{nameof(Record.SickLeaveId)}, " +
                                    $"{nameof(Record.DoctorId)}, " +
                                    $"{nameof(Record.PatientId)}, " +
                                    $"{nameof(Record.ModifiedDate)}, " +
                                    $"d.{nameof(Doctor.PositionId)}, " +
                                    $"d.{nameof(Doctor.UserId)}, " +
                                    $"ud.{nameof(User.FirstName)} as ud{nameof(User.FirstName)}, " +
                                    $"up.{nameof(User.FirstName)} as up{nameof(User.FirstName)} " +
                                    $"FROM {nameof(Record).ConvertToTableName()} as r  " +
                                    $"LEFT JOIN {nameof(Doctor).ConvertToTableName()} as d ON d.{nameof(Doctor.Id)} = r.{nameof(Record.DoctorId)} " +
                                    $"LEFT JOIN {nameof(Patient).ConvertToTableName()} as p ON p.{nameof(Patient.Id)} = r.{nameof(Record.PatientId)} " +
                                    $"LEFT JOIN {nameof(User).ConvertToTableName()} as ud ON ud.{nameof(User.Id)} = d.{nameof(Doctor.UserId)} " +
                                    $"LEFT JOIN {nameof(User).ConvertToTableName()} as up ON up.{nameof(User.Id)} = p.{nameof(Patient.UserId)}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
            model.Doctor.User.FirstName = (string)reader[$"ud{nameof(model.Doctor.User.FirstName)}"];

            return model;
        }
    }
}
