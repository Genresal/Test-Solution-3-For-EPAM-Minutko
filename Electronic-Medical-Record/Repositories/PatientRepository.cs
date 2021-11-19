using Electronic_Medical_Record.Helpers;
using Electronic_Medical_Record.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;

namespace Electronic_Medical_Record.Repositories
{
    public class PatientsRepository : BaseRepository<Patient>
    {
        public PatientsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Patient).MakePlular()}](
                                                         [{nameof(Patient.FirstName)}]
                                                        ,[{nameof(Patient.LastName)}]
                                                        ,[{nameof(Patient.Job)}]
                                                        ,[{nameof(Patient.PhoneNumber)}]
                                                        ,[{nameof(Patient.Birthday)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 1; i <= dataCount; i++)
            {
                string name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()();
                sqlExpression += "('" + name +
                    "','" + Gen.Random.Names.Surname()() +
                    "','" + Gen.Random.Text.Words()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.PhoneNumbers.WithRandomFormat()() +
                    "','" + Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)() +
                    "')";
                if (i != dataCount)
                {
                    sqlExpression += ",";
                }
            }
            SetDefaultData(sqlExpression);
        }

        protected override Patient Map(SqlDataReader reader)
        {
            var model = new Patient();

            model.Id = (int)reader[nameof(model.Id)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];
            model.Job = (string)reader[nameof(model.Job)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.Birthday = (DateTime)reader[nameof(model.Birthday)];

            return model;
        }
    }
}
