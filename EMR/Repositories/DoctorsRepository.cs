using ERM.Helpers;
using ERM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;

namespace ERM.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctor>
    {
        public DoctorsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Doctor).MakePlular()}](
                                                         [{nameof(Doctor.Position)}]
                                                        ,[{nameof(Doctor.Sex)}]
                                                        ,[{nameof(Doctor.FirstName)}]
                                                        ,[{nameof(Doctor.LastName)}]
                                                        ,[{nameof(Doctor.PhoneNumber)}]
                                                        ,[{nameof(Doctor.Birthday)}])
                                                    VALUES";
            int dataCount = 20;
            for (int i = 1; i <= dataCount; i++)
            {
                Sex sex;
                string name;
                if (i % 2 == 0)
                {
                    sex = Sex.Female;
                    name = Gen.Random.Names.Female()();
                }
                else
                {
                    sex = Sex.Male;
                    name = Gen.Random.Names.Male()();
                }

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}'" +
                    $",'{sex}'" +
                    $",'{name}'" +
                    $",'{Gen.Random.Names.Surname()()}'" +
                    $",'{Gen.Random.PhoneNumbers.WithRandomFormat()()}'" +
                    $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            SetDefaultData(sqlExpression);
        }

        protected override Doctor Map(SqlDataReader reader)
        {
            var model = new Doctor();

            model.Id = (int)reader[nameof(model.Id)];
            model.Sex = (string)reader[nameof(model.Sex)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];
            model.Position = (string)reader[nameof(model.Position)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.Birthday = (DateTime)reader[nameof(model.Birthday)];

            return model;
        }
    }
}
