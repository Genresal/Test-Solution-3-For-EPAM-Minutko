using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EMR.Data.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected string connectionString = null;
        protected BaseRepository(string conn)
        {
            connectionString = conn;
        }

        #region Base Queries
        public virtual IEnumerable<T> GetAll()
        {
            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()}";

            return ExecuteReader(sqlExpression);
        }

        public virtual IEnumerable<T> GetByColumn(string column, string value)
        {
            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE [{column}] = @value";

            return ExecuteReader(sqlExpression, new SqlParameter("@value", value));
        }

        public IEnumerable<T> GetByColumn(string column, List<string> values)
        {
            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE[{column}] IN (";

            List<SqlParameter> parameters = new List<SqlParameter>();
            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    sqlExpression = $"{sqlExpression}, ";
                }
                sqlExpression = $"{sqlExpression}@value{i}";

                parameters.Add(new SqlParameter($"@value{i}", values[i]));
            }

            sqlExpression = $"{sqlExpression})";

            return ExecuteReader(sqlExpression, parameters);
        }

        public virtual T GetById(int id)
        {
            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE [Id] = @Id";


            return ExecuteReader(sqlExpression, new SqlParameter("@Id", id)).FirstOrDefault();
        }

        public void Create(T item)
        {
            var properties = item.GetType()
                                  .GetProperties()
                                  .Where(x => x.PropertyType != typeof(BaseModel));

            string sqlExpression = $@"INSERT INTO {typeof(T).Name.ConvertToTableName()} (";

            foreach (var prop in properties)
            {
                sqlExpression = $"{sqlExpression}[{prop.Name}],";
            }

            sqlExpression = $"{sqlExpression})VALUES (";
            foreach (var prop in properties)
            {
                sqlExpression = $"{sqlExpression}[@{prop.Name}],";
            }

            sqlExpression += ")";

            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (var prop in properties)
            {
                if (prop.PropertyType != typeof(BaseModel))
                {
                    parameters.Add(new SqlParameter($"@{prop.Name}", item.GetType().GetProperty(prop.Name).GetValue(item, null)));
                }
            }

            ExecuteNonQuery(sqlExpression, parameters);
        }

        public void Update(T model)
        {
            var properties = model.GetType()
                                  .GetProperties()
                                  .Where(x => !x.PropertyType.IsSubclassOf(typeof(BaseModel)))
                                  .Where(x => x.Name != "Id")
                                  .ToList();
            string sqlExpression = $@"UPDATE {typeof(T).Name.ConvertToTableName()} SET ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            int lastArrayIndex = properties.Count - 1;
            for (int i = 0; i <= lastArrayIndex; i++)
            {
                var prop = properties[i];

                if (i > 0 )
                {
                    sqlExpression = $"{sqlExpression}, ";
                }

                sqlExpression = $"{sqlExpression}[{prop.Name}] = @{prop.Name}";

                parameters.Add(new SqlParameter($"@{prop.Name}", model.GetType().GetProperty(prop.Name).GetValue(model, null)));
            }

            parameters.Add(new SqlParameter($"@Id", model.Id));

            sqlExpression += " WHERE Id = @Id";

            ExecuteNonQuery(sqlExpression, parameters);
        }

        public virtual void Delete(int id)
        {
            string sqlExpression = $@"DELETE FROM {typeof(T).Name.ConvertToTableName()} WHERE Id = @Id";


            var parameter = new SqlParameter("@Id", id);
            ExecuteNonQuery(sqlExpression, parameter);
        }

        #endregion

        #region Table Service
        public void DropTable()
        {
            string sqlExpression = $@"DROP TABLE [{typeof(T).Name.ConvertToTableName()}];";
            ExecuteNonQuery(sqlExpression);
        }

        public bool IsTableExist()
        {
            string valueName = "result";
            string sqlExpression = $@"IF OBJECT_ID('dbo.{typeof(T).Name.ConvertToTableName()}', 'U') IS NOT NULL " +
                                                                                        $"SELECT 1 as {valueName} " +
                                                                                        $"ELSE " +
                      
                                                                                        $"SELECT 0 as {valueName}";
            return (int)ExecuteScalar(sqlExpression, valueName) == 1;
        }

        public bool IsTableHasRecords()
        {
            string valueName = "result";
            string sqlExpression = $"SELECT COUNT(*) as {valueName} " +
                                   $"FROM dbo.{typeof(T).Name.ConvertToTableName()}";

            return (int)ExecuteScalar(sqlExpression, valueName) > 1;
        }
        #endregion

        #region Abstract Methods
        protected abstract T Map(SqlDataReader reader);

        #endregion

        #region Exrecutors
        protected object ExecuteScalar(string sqlExpression, string valueName)
        {
            object result = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        result = reader[valueName];
                    }
                    connection.Close();
                }
            }

            return result;
        }

        protected List<T> ExecuteReader(string sqlExpression)
        {
            List<T> results = new List<T>();
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
                            results.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return results;
        }

        protected List<T> ExecuteReader(string sqlExpression, SqlParameter parameter)
        {
            List<T> results = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(parameter);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            results.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return results;
        }

        protected List<T> ExecuteReader(string sqlExpression, List<SqlParameter> parameters)
        {
            List<T> results = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            results.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return results;
        }

        protected void ExecuteNonQuery(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        protected void ExecuteNonQuery(string sqlExpression, SqlParameter parameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        protected void ExecuteNonQuery(string sqlExpression, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddRange(parameters.ToArray());
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        #endregion
    }
}