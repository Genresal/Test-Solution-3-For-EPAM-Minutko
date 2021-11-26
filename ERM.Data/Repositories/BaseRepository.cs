using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMR.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected string connectionString = null;
        public BaseRepository(string conn)
        {
            connectionString = conn;
        }

        public virtual IEnumerable<T> GetAll()
        {
            List<T> items = new List<T>();

            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()}";

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

        public IEnumerable<T> GetByColumn(string column, string value)
        {
            List<T> result = new List<T>();

            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE [{column}] = @value";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@value", value));
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return result;
        }

        public IEnumerable<T> GetByColumn(string column, List<string> values)
        {
            List<T> result = new List<T>();

            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE[{column}] IN (";

            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    sqlExpression = $"{sqlExpression}, ";
                }
                sqlExpression = $"{sqlExpression}@value{i}";
            }

            sqlExpression = $"{sqlExpression})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                for (int i = 0; i < values.Count; i++)
                { 
                    command.Parameters.Add(new SqlParameter($"@value{i}", values[i]));
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(Map(reader));
                        }
                    }
                }

                connection.Close();
            }
            return result;
        }

        public virtual T GetById(int id)
        {
            T result = default(T);

            string sqlExpression = $"SELECT * " +
                                   $"FROM {typeof(T).Name.ConvertToTableName()} " +
                                   $"WHERE [Id] = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = (Map(reader));
                    }
                }

                connection.Close();
            }
            return result;
        }
        //public abstract void CreateTable();

        public void DropTable()
        {
            string sqlExpression = $@"DROP TABLE [{typeof(T).Name.ConvertToTableName()}];";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
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

        public void Create(T model)
        {
            var properties = model.GetType().GetProperties();
            string sqlExpression = $@"INSERT INTO {typeof(T).Name.ConvertToTableName()} (";

            foreach (var prop in properties)
            {
                sqlExpression += $"[{prop.Name}],";
            }

            sqlExpression += ")VALUES (";
            foreach (var prop in properties)
            {
                sqlExpression += $"[@{prop.Name}],";
            }

            sqlExpression += ")";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                foreach (var prop in properties)
                {
                    command.Parameters.Add(new SqlParameter($"@{prop.Name}", model.GetType().GetProperty(prop.Name).GetValue(model, null)));
                }
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(T model)
        {
            var properties = model.GetType().GetProperties();
            string sqlExpression = $@"UPDATE {typeof(T).Name.ConvertToTableName()} SET";

            int lastArrayIndex = properties.Length - 1;
            for (int i = 0; i <= lastArrayIndex; i++)
            {
                var prop = properties[i];
                if (prop.Name == "Id")
                {
                    continue;
                }

                sqlExpression += $"[{prop.Name}] = @{prop.Name}";

                if (i < lastArrayIndex)
                {
                    sqlExpression += ", ";
                }
            }

            sqlExpression += " WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                foreach (var prop in properties)
                {
                    command.Parameters.Add(new SqlParameter($"@{prop.Name}", model.GetType().GetProperty(prop.Name).GetValue(model, null)));
                }
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = $@"DELETE FROM {typeof(T).Name.ConvertToTableName()} WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void CreateDefaultDate()
        {
            if (!IsTableHasRecords())
            {
                SetDefaultData();
            }
        }

        public bool IsTableExist()
        {
            string sqlExpression = $@"IF OBJECT_ID('dbo.{typeof(T).Name.ConvertToTableName()}', 'U') IS NOT NULL " +
                                                                                        "SELECT 1 as 'Result' " +
                                                                                        "ELSE " +
                                                                                        "SELECT 0 as 'Result'";
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["Result"];
                }
                connection.Close();
            }

            return result == 1;
        }

        public bool IsTableHasRecords()
        {
            string sqlExpression = $"SELECT COUNT(*) as result " + 
                                   $"FROM dbo.{typeof(T).Name.ConvertToTableName()}";
            int result = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    result = (int)reader["result"];
                }
                connection.Close();
            }

            return result > 1;
        }

        protected abstract T Map(SqlDataReader reader);
        public abstract void SetDefaultData();

        public virtual void SetDefaultData(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}