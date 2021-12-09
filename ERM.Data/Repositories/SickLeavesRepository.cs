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
    public class SickLeavesRepository : BaseRepository<SickLeave>, IRepository<SickLeave>
    {
        public SickLeavesRepository(string conn) : base (conn)
        {
        }

        public override IEnumerable<SickLeave> GetAll()
        {
            return new List<SickLeave>();
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(SickLeave).ConvertToTableName()}](
                                                         [{nameof(SickLeave.Number)}]
                                                        ,[{nameof(SickLeave.StartDate)}]
                                                        ,[{nameof(SickLeave.FinalDate)}])
                                                    VALUES";
            int dataCount = 200;
            for (int i = 1; i <= dataCount; i++)
            {
                DateTime startDate = Gen.Random.Time.Dates(DateTime.Now.AddYears(-5), DateTime.Now)();
                DateTime endDate = startDate.AddDays(Gen.Random.Numbers.Integers(1, 30)());

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Numbers.Integers(1000, 1000000)()}'" +
                    $",'{startDate}'" +
                    $",'{endDate}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            ExecuteNonQuery(sqlExpression);
        }

        public override void Create(SickLeave item, int relationId)
        {
            var properties = item.GetType()
                .GetProperties()
                .Where(x => x.Name != "Id")
                .ToList();

            var parameters = ProrertiesToSqlParameters(item, properties);
            parameters.Add(new SqlParameter(nameof(RecordTreatment.RecordId), relationId));

            StoredExecuteNonQuery("CreateSIckLeave", parameters);
        }

        public override void Delete(int id)
        {
            StoredExecuteNonQuery("DeleteSickLeave", new SqlParameter("@Id", id));
        }

        protected override SickLeave Map(SqlDataReader reader)
        {
            var model = new SickLeave();

            model.Id = (int)reader[nameof(model.Id)];
            model.Number = Convert.IsDBNull(reader[nameof(model.Number)]) ? null : (string)reader[nameof(model.Number)];
            model.StartDate = (DateTime)reader[nameof(model.StartDate)];
            model.FinalDate = (DateTime)reader[nameof(model.FinalDate)];

            return model;
        }
    }
}
