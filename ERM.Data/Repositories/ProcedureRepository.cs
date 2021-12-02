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
    public class ProcedureRepository : BaseRepository<Procedure>, IRepository<Procedure>
    {
        public ProcedureRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Procedure).ConvertToTableName()}](
                                                         [{nameof(Procedure.Name)}]
                                                        ,[{nameof(Procedure.Description)}])
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

        protected override Procedure Map(SqlDataReader reader)
        {
            var model = new Procedure();

            model.Id = (int)reader[nameof(model.Id)];
            model.Name = (string)reader[nameof(model.Name)];
            model.Description = (string)reader[nameof(model.Description)];

            return model;
        }
    }
}
