using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using RandomGen;
using System.Data.SqlClient;

namespace EMR.Data.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IRepository<Position>
    {
        public PositionRepository(string conn) : base(conn)
        {
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Position).ConvertToTableName()}](
                                                        [{nameof(Position.Name)}])
                                                    VALUES";
            int dataCount = 10;
            for (int i = 1; i <= dataCount; i++)
            {

                sqlExpression = $"{sqlExpression}" +
                    $"('{Gen.Random.Text.Words()().MakeFirstCharUppercase()}')";
                if (i != dataCount)
                {
                    sqlExpression = $"{sqlExpression},";
                }
            }
            ExecuteNonQuery(sqlExpression);
        }

        protected override Position Map(SqlDataReader reader)
        {
            var model = new Position();

            model.Id = (int)reader[nameof(model.Id)];
            model.Name = (string)reader[nameof(model.Name)];

            return model;
        }
    }
}
