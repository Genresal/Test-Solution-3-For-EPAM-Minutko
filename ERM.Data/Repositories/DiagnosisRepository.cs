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
    public class DiagnosisRepository : BaseRepository<Diagnosis>, IRepository<Diagnosis>
    {
        public DiagnosisRepository(string conn) : base(conn)
        {
        }

        public override IEnumerable<Diagnosis> GetAll()
        {
            return new List<Diagnosis>();
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Diagnosis).ConvertToTableName()}](
                                                        [{nameof(Diagnosis.Name)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 1; i <= dataCount; i++)
            {

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Short()().MakeFirstCharUppercase()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override Diagnosis Map(SqlDataReader reader)
        {
            var model = new Diagnosis();

            model.Id = (int)reader[nameof(model.Id)];
            model.Name = (string)reader[nameof(model.Name)];

            return model;
        }
    }
}
