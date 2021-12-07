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
            string sqlExpression = $"BEGIN TRY " +
                                    $"BEGIN TRAN " +
                                    $"INSERT INTO [dbo].[tSickLeave]" +
                                    $"([Number]" +
                                    $",[StartDate]" +
                                    $",[FinalDate])" +
                                    $"VALUES" +
                                    $"(@Number" +
                                    $",@StartDate" +
                                    $",@FinalDate) " +
                                    $"UPDATE [dbo].[tRecord] " +
                                    $"SET [SickLeaveId] = @@IDENTITY " +
                                    $"WHERE Id = @RecordId " +
                                    $"COMMIT TRAN " +
                                    $"END TRY " +
                                    $"BEGIN CATCH " +
                                    $"ROLLBACK TRAN " +
                                    $"END CATCH";

            var properties = item.GetType()
          .GetProperties()
          .Where(x => x.Name != "Id")
          .ToList();

            var parameters = ProrertiesToSqlParameters(item, properties);
            parameters.Add(new SqlParameter(nameof(RecordTreatment.RecordId), relationId));

            ExecuteNonQuery(sqlExpression, parameters);
        }

        public override void Delete(int id)
        {
            string sqlExpression = $"BEGIN TRY " +
                                    $"BEGIN TRAN " +
                                    $"UPDATE [dbo].[tRecord] " +
                                    $"SET [SickLeaveId] = null " +
                                    $"WHERE SickLeaveId = @Id " +
                                    $"DELETE FROM [dbo].[tSickLeave] " +
                                    $"WHERE Id = @Id " +
                                    $"COMMIT TRAN " +
                                    $"END TRY " +
                                    $"BEGIN CATCH " +
                                    $"ROLLBACK TRAN " +
                                    $"END CATCH";

            var parameter = new SqlParameter("@Id", id);

            ExecuteNonQuery(sqlExpression, parameter);
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
