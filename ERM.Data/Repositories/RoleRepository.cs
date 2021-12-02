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
    public class RoleRepository : BaseRepository<Role>, IRepository<Role>
    {
        public RoleRepository(string conn) : base (conn)
        {
        }

        public override IEnumerable<Role> GetAll()
        {
            return new List<Role>();
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
