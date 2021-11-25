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
    public class PositionRepository : BaseRepository<Position>
    {
        public PositionRepository(string conn) : base(conn)
        {
        }
        /*
        public override IEnumerable<Position> GetAll()
        {
            List<Position> items = new List<Position>();

            string sqlExpression = $"SELECT * " +
                                   $"FROM {nameof(Position).ConvertToTableName()}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            items.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return items;
        }
        */

        public override void SetDefaultData()
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
            SetDefaultData(sqlExpression);
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
