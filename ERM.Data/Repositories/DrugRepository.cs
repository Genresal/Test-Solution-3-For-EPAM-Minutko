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
    public class DrugRepository : BaseRepository<Drug>, IRepository<Drug>
    {
        public DrugRepository(string conn) : base (conn)
        {
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Drug).ConvertToTableName()}](
                                                         [{nameof(Drug.Name)}]
                                                        ,[{nameof(Drug.Description)}])
                                                    VALUES";
            int dataCount = 80;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Short()()}'" +
                    $",'{Gen.Random.Text.Long()()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            ExecuteNonQuery(sqlExpression);
        }

        public override void Create(Drug item, int relationId)
        {
            var properties = item.GetType()
                .GetProperties()
                .Where(x => x.Name != "Id")
                .ToList();

            var parameters = ProrertiesToSqlParameters(item, properties);
            parameters.Add(new SqlParameter(nameof(RecordTreatment.RecordId), relationId));

            StoredExecuteNonQuery("CreateDrug", parameters);
        }

        protected override Drug Map(SqlDataReader reader)
        {
            var model = new Drug();

            model.Id = (int)reader[nameof(model.Id)];
            model.Name = (string)reader[nameof(model.Name)];
            model.Description = (string)reader[nameof(model.Description)];

            return model;
        }
    }
}
