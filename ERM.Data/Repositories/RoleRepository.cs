using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using System.Data.SqlClient;

namespace EMR.Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRepository<Role>
    {
        public RoleRepository(string conn) : base(conn)
        {
        }

        public void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Role).ConvertToTableName()}](
                                                        [{nameof(Role.Name)}])
                                                    VALUES";

            sqlExpression = $"{sqlExpression}" +
            $"('User'), ('Doctor'), ('Editor'), ('Admin')";
            ExecuteNonQuery(sqlExpression);
        }

        protected override Role Map(SqlDataReader reader)
        {
            var model = new Role();

            model.Id = (int)reader[nameof(model.Id)];
            model.Name = (string)reader[nameof(model.Name)];

            return model;
        }
    }
}
