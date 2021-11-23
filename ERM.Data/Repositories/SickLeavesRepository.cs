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
    public class SickLeavesRepository : BaseRepository<SickLeave>
    {
        public SickLeavesRepository(string conn) : base (conn)
        {
        }

        public override IEnumerable<SickLeave> GetAll()
        {
            return new List<SickLeave>();
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(SickLeave).ConvertToTableName()}](
                                                        ,[{nameof(SickLeave.Number)}]
                                                        ,[{nameof(SickLeave.StartDate)}]
                                                        ,[{nameof(SickLeave.FinalDate)}])
                                                    VALUES";
            int dataCount = 300;
            for (int i = 1; i <= dataCount; i++)
            {
                DateTime startDate = Gen.Random.Time.Dates(DateTime.Now.AddYears(-5), DateTime.Now)();
                DateTime endDate = startDate.AddDays(Gen.Random.Numbers.Integers(1, 30)());

                sqlExpression = $"{sqlExpression}('{ Gen.Random.Numbers.Integers(1, 200)()}'" +
                    $",'{Gen.Random.Numbers.Integers(1000, 1000000)()}'" +
                    $",'{startDate}'" +
                    $",'{endDate}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            SetDefaultData(sqlExpression);
        }

        protected override SickLeave Map(SqlDataReader reader)
        {
            var model = new SickLeave();

            model.Id = (int)reader[nameof(model.Id)];
            model.Number = (string)reader[nameof(model.Number)];
            model.StartDate = (DateTime)reader[nameof(model.StartDate)];
            model.FinalDate = (DateTime)reader[nameof(model.FinalDate)];

            return model;
        }
    }
}
