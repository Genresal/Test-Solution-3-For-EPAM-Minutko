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
    public class DoctorsRepository : BaseRepository<Doctor>, IRepository<Doctor>
    {
        private readonly string baseQuery;
        public DoctorsRepository(string conn) : base (conn)
        {
            baseQuery = $"SELECT d.{nameof(Patient.Id)}, " +
  $"{nameof(Doctor.UserId)}, " +
  $"{nameof(Doctor.PositionId)}, " +
  $"up.{nameof(User.Login)} as {nameof(Doctor)}{nameof(User.Login)}, " +
  $"up.{nameof(User.Password)} as {nameof(Doctor)}{nameof(User.Password)}, " +
  $"up.{nameof(User.FirstName)} as {nameof(Doctor)}{nameof(User.FirstName)}, " +
  $"up.{nameof(User.LastName)} as {nameof(Doctor)}{nameof(User.LastName)}, " +
  $"up.{nameof(User.RoleId)} as {nameof(Doctor)}{nameof(User.RoleId)}, " +
  $"up.{nameof(User.Birthday)} as {nameof(Doctor)}{nameof(User.Birthday)}, " +
  $"up.{nameof(User.Email)} as {nameof(Doctor)}{nameof(User.Email)}, " +
  $"up.{nameof(User.PhoneNumber)} as {nameof(Doctor)}{nameof(User.PhoneNumber)}, " +
  $"up.{nameof(User.PhotoUrl)} as {nameof(Doctor)}{nameof(User.PhotoUrl)}, " +
  $"dp.{nameof(Position.Name)} as {nameof(Position)}{nameof(Position.Name)} " +
  $"FROM {nameof(Doctor).ConvertToTableName()} as d " +
  $"LEFT JOIN {nameof(User).ConvertToTableName()} as up ON up.{nameof(User.Id)} = d.{nameof(Patient.UserId)} " +
  $"LEFT JOIN {nameof(Position).ConvertToTableName()} as dp ON dp.{nameof(Position.Id)} = d.{nameof(Patient.Id)}";
        }

        public override IEnumerable<Doctor> GetAll()
        {
            return ExecuteReader(baseQuery);
        }

        public override IEnumerable<Doctor> GetByColumn(string column, string value)
        {
            string sqlExpression = $"{baseQuery} WHERE [{column}] = @value";
            return ExecuteReader(sqlExpression, new SqlParameter("@value", value));
        }

        public override Doctor GetById(int id)
        {
            string sqlExpression = $"{baseQuery} WHERE p.Id = @id";
            return ExecuteReader(sqlExpression, new SqlParameter("@id", id)).FirstOrDefault();
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Doctor).ConvertToTableName()}](
                                                         [{nameof(Doctor.UserId)}]
                                                        ,[{nameof(Doctor.PositionId)}])
                                                    VALUES";
            int dataCount = 20;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}" +
                    $"('{i}'" +
                    $",'{Gen.Random.Numbers.Integers(1, 10)()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override Doctor Map(SqlDataReader reader)
        {
            var model = new Doctor();

            model.Id = (int)reader[nameof(model.Id)];
            model.UserId = (int)reader[nameof(model.UserId)];
            model.PositionId = (int)reader[nameof(model.PositionId)];
            model.User.Login = (string)reader[$"{nameof(Doctor)}{nameof(model.User.Login)}"];
            model.User.Password = (string)reader[$"{nameof(Doctor)}{nameof(model.User.Password)}"];
            model.User.FirstName = (string)reader[$"{nameof(Doctor)}{nameof(model.User.FirstName)}"];
            model.User.LastName = (string)reader[$"{nameof(Doctor)}{nameof(model.User.LastName)}"];
            model.User.RoleId = (int)reader[$"{nameof(Doctor)}{nameof(model.User.RoleId)}"];
            model.User.Birthday = (DateTime)reader[$"{nameof(Doctor)}{nameof(model.User.Birthday)}"];
            model.User.Email = (string)reader[$"{nameof(Doctor)}{nameof(model.User.Email)}"];
            model.User.PhoneNumber = (string)reader[$"{nameof(Doctor)}{nameof(model.User.PhoneNumber)}"];
            model.User.PhotoUrl = (string)reader[$"{nameof(Doctor)}{nameof(model.User.PhotoUrl)}"];
            model.Position.Name = (string)reader[$"{nameof(Position)}{nameof(Position.Name)}"];

            return model;
        }
    }
}
