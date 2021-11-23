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