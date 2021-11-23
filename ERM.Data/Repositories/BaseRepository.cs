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

        public abstract IEnumerable<T> GetAll();

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
        public void CheckTable()
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