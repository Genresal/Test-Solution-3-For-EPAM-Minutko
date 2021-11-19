using Electronic_Medical_Record.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Electronic_Medical_Record.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected string connectionString = null;
        public BaseRepository(string conn)
        {
            connectionString = conn;
            CheckTable();
        }

        public IEnumerable<T> GetAll()
        {
            List<T> items = new List<T>();

            string sqlExpression = $"SELECT * FROM {typeof(T).Name.MakePlular()}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(Map(reader));
                    }
                }
                connection.Close();
            }
            return items;
        }

        public IEnumerable<T> GetByField(string field, string value)
        {
            List<T> items = new List<T>();

            string sqlExpression = $"SELECT * FROM {typeof(T).Name.MakePlular()} WHERE {field} = '{value}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(Map(reader));
                    }
                }
                connection.Close();
            }
            return items;
        }

        public T FindById(int id)
        {
            T item = default(T);
            string sqlExpression = $@"SELECT * FROM {typeof(T).Name.MakePlular()} WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    item = Map(reader);
                }
                connection.Close();
            }
            return item;
        }

        public void Create(T model)
        {
            var properties = model.GetType().GetProperties();
            string sqlExpression = $@"INSERT INTO {typeof(T).Name.MakePlular()} (";

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
            string sqlExpression = $@"UPDATE {typeof(T).Name.MakePlular()} SET";

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
    string sqlExpression = $@"DELETE FROM {typeof(T).Name.MakePlular()} WHERE Id = @Id";
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
public void CheckTable()
{
    if (!IsTableExist())
    {
        CreateTable<T>();
        SetDefaultData();
    }
}

public bool IsTableExist()
{
    string sqlExpression = $@"IF OBJECT_ID('dbo.{typeof(T).Name.MakePlular()}', 'U') IS NOT NULL " +
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

    return result == 1 ? true : false;
}

public void CreateTable<T>()
{
    string sqlExpression = $@"CREATE TABLE [dbo].[{typeof(T).Name.MakePlular()}](";

    foreach (var prop in typeof(T).GetProperties())
    {
        if (prop.Name == "Id")
        {
            sqlExpression += $"[Id] [int] IDENTITY(1, 1) ";
        }
        else
        {
            sqlExpression += $"[{prop.Name}] ";
            switch (Type.GetTypeCode(prop.PropertyType))
            {
                case TypeCode.Int32:
                    sqlExpression += "[int] ";
                    break;
                case TypeCode.String:
                    sqlExpression += "[nvarchar](255) ";
                    break;
                case TypeCode.DateTime:
                    sqlExpression += "[datetime] ";
                    break;
            }

        }
        sqlExpression += "NOT NULL, ";
    }

    sqlExpression += $"CONSTRAINT[PK_{typeof(T).Name.MakePlular()}] PRIMARY KEY CLUSTERED([Id] ASC))";


    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlCommand command = new SqlCommand(sqlExpression, connection);
        command.ExecuteNonQuery();

        connection.Close();
    }
}

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