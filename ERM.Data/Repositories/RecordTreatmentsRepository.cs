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
    public class RecordTreatmentsRepository : BaseRepository<RecordTreatment>
    {
        public RecordTreatmentsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(RecordTreatment).ConvertToTableName()}](
                                                         [{nameof(RecordTreatment.RecordId)}]
                                                        ,[{nameof(RecordTreatment.DrugId)}]
                                                        ,[{nameof(RecordTreatment.ProcedureId)}])
                                                    VALUES";
            int dataCount = 300;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}('{Gen.Random.Numbers.Integers(1, 200)()}'" +
                    $",'{Gen.Random.Numbers.Integers(1, 80)()}'" +
                    $",'{Gen.Random.Numbers.Integers(1, 80)()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            SetDefaultData(sqlExpression);
        }

        protected override RecordTreatment Map(SqlDataReader reader)
        {
            var model = new RecordTreatment();

            model.Id = (int)reader[nameof(model.Id)];
            model.RecordId = (int)reader[nameof(model.RecordId)];
            model.DrugId = (int)reader[nameof(model.DrugId)];
            model.ProcedureId = (int)reader[nameof(model.ProcedureId)];

            return model;
        }
    }
}
