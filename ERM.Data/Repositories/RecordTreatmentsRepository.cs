using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using RandomGen;
using System;
using System.Data.SqlClient;

namespace EMR.Data.Repositories
{
    public class RecordTreatmentsRepository : BaseRepository<RecordTreatment>, IRepository<RecordTreatment>
    {
        public RecordTreatmentsRepository(string conn) : base(conn)
        {
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(RecordTreatment).ConvertToTableName()}](
                                                         [{nameof(RecordTreatment.RecordId)}]
                                                        ,[{nameof(RecordTreatment.DrugId)}]
                                                        ,[{nameof(RecordTreatment.ProcedureId)}])
                                                    VALUES";
            int dataCount = 700;
            for (int i = 1; i <= dataCount; i++)
            {
                string randomNumString = Gen.Random.Numbers.Integers(1, 80)().ToString();
                string drugId = i % 2 == 0 ? "null" : randomNumString;
                string procedureId = drugId.Equals("null") ? randomNumString : "null";

                sqlExpression = $"{sqlExpression}('{Gen.Random.Numbers.Integers(1, 200)()}'" +
                    $",{drugId}" +
                    $",{procedureId})";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }

            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override RecordTreatment Map(SqlDataReader reader)
        {
            var model = new RecordTreatment();

            model.Id = (int)reader[nameof(model.Id)];
            model.RecordId = (int)reader[nameof(model.RecordId)];
            model.DrugId = Convert.IsDBNull(reader[nameof(model.DrugId)]) ? null : (int?)reader[nameof(model.DrugId)];
            model.ProcedureId = Convert.IsDBNull(reader[nameof(model.ProcedureId)]) ? null : (int?)reader[nameof(model.ProcedureId)];


            return model;
        }
    }
}
