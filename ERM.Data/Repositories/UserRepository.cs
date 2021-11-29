using EMR.Data.Helpers;
using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;

namespace EMR.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(string conn) : base(conn)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            return new List<User>();
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(User).ConvertToTableName()}](
                                                         [{nameof(User.Login)}]
                                                        ,[{nameof(User.Password)}]
                                                        ,[{nameof(User.RoleId)}]
                                                        ,[{nameof(User.FirstName)}]
                                                        ,[{nameof(User.LastName)}]
                                                        ,[{nameof(User.Birthday)}]
                                                        ,[{nameof(User.PhoneNumber)}]
                                                        ,[{nameof(User.PhotoUrl)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 1; i <= dataCount; i++)
            {
                string name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()();

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}'" +
                    $",'{Gen.Random.Text.Words()()}'" +
                    $",'{Gen.Random.Numbers.Integers(1, 4)()}'" +
                    $",'{name}'" +
                    $",'{Gen.Random.Names.Surname()()}'" +
                    $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}'" +
                    $",'{Gen.Random.PhoneNumbers.WithRandomFormat()()}'" +
                    $",'{Gen.Random.Internet.Urls()()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override User Map(SqlDataReader reader)
        {
            var model = new User();

            model.Id = (int)reader[nameof(model.Id)];
            model.Login = (string)reader[nameof(model.Login)];
            model.Password = (string)reader[nameof(model.Password)];
            model.RoleId = (int)reader[nameof(model.RoleId)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];
            model.Birthday = (DateTime)reader[nameof(model.Birthday)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.PhotoUrl = (string)reader[nameof(model.PhotoUrl)];

            return model;
        }
    }
}
