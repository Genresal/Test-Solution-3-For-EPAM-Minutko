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
    public class UserRepository : BaseRepository<User>, IRepository<User>
    {
        private readonly string baseQuery;
        public UserRepository(string conn) : base(conn)
        {
            baseQuery = $"SELECT u.{nameof(User.Id)}, " +
                        $"{nameof(User.Login)}, " +
                        $"{nameof(User.Password)}, " +
                        $"{nameof(User.FirstName)}, " +
                        $"{nameof(User.LastName)}, " +
                        $"{nameof(User.RoleId)}, " +
                        $"{nameof(User.Birthday)}, " +
                        $"{nameof(User.Email)}, " +
                        $"{nameof(User.PhoneNumber)}, " +
                        $"{nameof(User.PhotoUrl)}, " +
                        $"r.{nameof(Role.Name)} as {nameof(Role)}{nameof(Role.Name)} " +
                        $"FROM {nameof(User).ConvertToTableName()} as u " +
                        $"LEFT JOIN {nameof(Role).ConvertToTableName()} as r ON r.{nameof(Role.Id)} = u.{nameof(User.RoleId)}";
        }

        public override IEnumerable<User> GetAll()
        {
            return ExecuteReader(baseQuery);
        }

        public override IEnumerable<User> GetByColumn(string column, string value)
        {
            string sqlExpression = $"{baseQuery} WHERE [{column}] = @value";
            return ExecuteReader(sqlExpression, new SqlParameter("@value", value));
        }

        public override User GetById(int id)
        {
            string sqlExpression = $"{baseQuery} WHERE p.Id = @id";
            return ExecuteReader(sqlExpression, new SqlParameter("@id", id)).FirstOrDefault();
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(User).ConvertToTableName()}](
                                                         [{nameof(User.Login)}]
                                                        ,[{nameof(User.Password)}]
                                                        ,[{nameof(User.RoleId)}]
                                                        ,[{nameof(User.FirstName)}]
                                                        ,[{nameof(User.LastName)}]
                                                        ,[{nameof(User.Birthday)}]
                                                        ,[{nameof(User.Email)}]
                                                        ,[{nameof(User.PhoneNumber)}]
                                                        ,[{nameof(User.PhotoUrl)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 1; i <= dataCount; i++)
            {
                string name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()();
                int roleId = i > 20 ? 1 : 2;

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}'" +
                    $",'{Gen.Random.Text.Words()()}'" +
                    $",'{roleId}'" +
                    $",'{name}'" +
                    $",'{Gen.Random.Names.Surname()()}'" +
                    $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}'" +
                    $",'{Gen.Random.Internet.EmailAddresses()()}'" +
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
            model.Email = (string)reader[nameof(model.Email)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.PhotoUrl = Convert.IsDBNull(reader[nameof(model.PhotoUrl)]) ? null : (string?)reader[nameof(model.PhotoUrl)];
            model.Role.Name = (string)reader[$"{nameof(Role)}{nameof(Role.Name)}"];

            return model;
        }
    }
}
