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
    public class DoctorsRepository : BaseRepository<Doctor>
    {
        public DoctorsRepository(string conn) : base (conn)
        {
        }

        public override IEnumerable<Doctor> GetAll()
        {
            return new List<Doctor>();
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
            SetDefaultData(sqlExpression);
        }

        protected override Doctor Map(SqlDataReader reader)
        {
            var model = new Doctor();

            model.Id = (int)reader[nameof(model.Id)];
            model.UserId = (int)reader[nameof(model.UserId)];
            model.PositionId = (int)reader[nameof(model.PositionId)];

            return model;
        }
    }
}
