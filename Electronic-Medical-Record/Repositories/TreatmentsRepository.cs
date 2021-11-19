using Electronic_Medical_Record.Helpers;
using Electronic_Medical_Record.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;

namespace Electronic_Medical_Record.Repositories
{
    public class TreatmentsRepository : BaseRepository<Treatment>
    {
        public TreatmentsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Treatment).MakePlular()}](
                                                         [{nameof(Treatment.RecordId)}]
                                                        ,[{nameof(Treatment.Drug)}]
                                                        ,[{nameof(Treatment.DrugUsage)}]
                                                        ,[{nameof(Treatment.Procedure)}]
                                                        ,[{nameof(Treatment.ModifyingDate)}])
                                                    VALUES";
            int dataCount = 20;
            for (int i = 1; i <= dataCount; i++)
            {
                sqlExpression += "('" + Gen.Random.Numbers.Integers(1, 200)() +
                    "','" + Gen.Random.Text.Words()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.Text.Long()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.Text.Long()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.Time.Dates(DateTime.Now.AddYears(-5), DateTime.Now)() +
                    "')";
                if (i != dataCount)
                {
                    sqlExpression += ",";
                }

            }
            SetDefaultData(sqlExpression);
        }

        protected override Treatment Map(SqlDataReader reader)
        {
            var model = new Treatment();

            model.Id = (int)reader[nameof(model.Id)];
            model.RecordId = (int)reader[nameof(model.RecordId)];
            model.Drug = (string)reader[nameof(model.Drug)];
            model.DrugUsage = (string)reader[nameof(model.DrugUsage)];
            model.Procedure = (string)reader[nameof(model.Procedure)];
            model.ModifyingDate = (DateTime)reader[nameof(model.ModifyingDate)];

            return model;
        }
    }
}
