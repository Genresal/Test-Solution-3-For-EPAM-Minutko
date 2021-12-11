using EMR.Business.Helpers;
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
    public class UserRepository : BaseRepository<User>, IRepository<User>
    {
        public UserRepository(string conn) : base(conn)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", DBNull.Value));
            parameters.Add(new SqlParameter("@OPERATOR", DBNull.Value));
            parameters.Add(new SqlParameter("@VALUE", DBNull.Value));
            return StoredExecuteReader("GetUsers", parameters);
        }

        public override IEnumerable<User> GetByColumn(string column, string value)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", column));
            parameters.Add(new SqlParameter("@OPERATOR", "="));
            parameters.Add(new SqlParameter("@VALUE", value));
            return StoredExecuteReader("GetUsers", parameters);
        }

        public override User GetById(int id)
        {
            return GetByColumn("u.Id", id).FirstOrDefault();
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
                    $",'{Gen.Random.Text.Words()().HashString()}'" +
                    $",'{roleId}'" +
                    $",'{name}'" +
                    $",'{Gen.Random.Names.Surname()()}'" +
                    $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}'" +
                    $",'{Gen.Random.Internet.EmailAddresses()()}'" +
                    $",'{Gen.Random.PhoneNumbers.WithRandomFormat()()}'" +
                    $",'https://azuresklad.blob.core.windows.net/images/{i}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }

            sqlExpression = $"{sqlExpression}," +
    $"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}'" +
    $",'{Gen.Random.Text.Words()().HashString()}'" +
    $",'{3}'" +
    $",'{Gen.Random.Names.Male()()}'" +
    $",'{Gen.Random.Names.Surname()()}'" +
    $",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}'" +
    $",'{Gen.Random.Internet.EmailAddresses()()}'" +
    $",'{Gen.Random.PhoneNumbers.WithRandomFormat()()}'" +
    $",'https://azuresklad.blob.core.windows.net/images/101')";

            sqlExpression = $"{sqlExpression}," +
$"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}'" +
$",'{Gen.Random.Text.Words()().HashString()}'" +
$",'{4}'" +
$",'{Gen.Random.Names.Male()()}'" +
$",'{Gen.Random.Names.Surname()()}'" +
$",'{Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)()}'" +
$",'{Gen.Random.Internet.EmailAddresses()()}'" +
$",'{Gen.Random.PhoneNumbers.WithRandomFormat()()}'" +
$",'https://azuresklad.blob.core.windows.net/images/102')";
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
