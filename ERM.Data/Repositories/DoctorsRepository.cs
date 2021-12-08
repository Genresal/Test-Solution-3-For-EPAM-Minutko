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
    public class DoctorsRepository : BaseRepository<Doctor>, IRepository<Doctor>
    {
        public DoctorsRepository(string conn) : base(conn)
        {
        }

        public override IEnumerable<Doctor> GetAll()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", DBNull.Value));
            parameters.Add(new SqlParameter("@OPERATOR", DBNull.Value));
            parameters.Add(new SqlParameter("@VALUE", DBNull.Value));
            return StoredExecuteReader("GetDoctors", parameters);
        }

        public override IEnumerable<Doctor> GetByColumn(string column, string value)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@COLUMN", column));
            parameters.Add(new SqlParameter("@OPERATOR", "="));
            parameters.Add(new SqlParameter("@VALUE", value));
            return StoredExecuteReader("GetDoctors", parameters);
        }

        public override Doctor GetById(int id)
        {
            return GetByColumn("doc.Id", id).FirstOrDefault();
        }

        public override void Create(Doctor model)
        {
            var userProperties = model.User.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .Where(x => x.Name != "Id")
                        .ToList();

            var parameters = ProrertiesToSqlParameters(model.User, userProperties);
            parameters.Add(new SqlParameter(nameof(model.PositionId), model.PositionId));

            StoredExecuteNonQuery("CreateDoctor", parameters);
        }

        public override void Update(Doctor model)
        {
            var doctorProperties = model.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .ToList();

            var userProperties = model.User.GetType()
                        .GetProperties()
                        .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                        .Where(x => x.Name != "Id")
                        .ToList();

            var parameters = ProrertiesToSqlParameters(model, doctorProperties);
            parameters.AddRange(ProrertiesToSqlParameters(model.User, userProperties));

            StoredExecuteNonQuery("UpdateDoctor", parameters);
        }

        public override void Delete(int id)
        {
            StoredExecuteNonQuery("DeleteDoctor", new SqlParameter("@Id", id));
        }

        public void SetDefaultData()
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
            model.User.PhotoUrl = Convert.IsDBNull(reader[$"{nameof(Doctor)}{nameof(model.User.PhotoUrl)}"]) ? null : (string)reader[$"{nameof(Doctor)}{nameof(model.User.PhotoUrl)}"];
            model.Position.Name = (string)reader[$"{nameof(Position)}{nameof(Position.Name)}"];

            return model;
        }
    }
}
