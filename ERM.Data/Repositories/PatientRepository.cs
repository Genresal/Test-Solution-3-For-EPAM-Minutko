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
    public class PatientsRepository : BaseRepository<Patient>
    {
        public PatientsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Patient).ConvertToTableName()}](
                                                         [{nameof(Patient.UserId)}]
                                                        ,[{nameof(Patient.Job)}])
                                                    VALUES";
            int dataCount = 100;
            for (int i = 21; i <= dataCount; i++)
            {
                sqlExpression = $"{sqlExpression}" +
                    $"('{i}'" +
                    $",'{Gen.Random.Text.Short()()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }
            SetDefaultData(sqlExpression);
        }

        protected override Patient Map(SqlDataReader reader)
        {
            var model = new Patient();

            model.Id = (int)reader[nameof(model.Id)];
            model.Job = (string)reader[nameof(model.Job)];

            return model;
        }
    }
}
