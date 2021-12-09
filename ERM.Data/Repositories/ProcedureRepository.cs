using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using RandomGen;
using System.Data.SqlClient;
using System.Linq;

namespace EMR.Data.Repositories
{
    public class ProcedureRepository : BaseRepository<Procedure>, IRepository<Procedure>
    {
        public ProcedureRepository(string conn) : base(conn)
        {
        }

        public void SetDefaultData()
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

        public override void Create(Procedure item, int relationId)
        {
            var properties = item.GetType()
                .GetProperties()
                .Where(x => x.Name != "Id")
                .ToList();

            var parameters = ProrertiesToSqlParameters(item, properties);
            parameters.Add(new SqlParameter(nameof(RecordTreatment.RecordId), relationId));

            StoredExecuteNonQuery("CreateProcedure", parameters);
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
