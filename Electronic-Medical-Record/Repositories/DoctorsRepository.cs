using Electronic_Medical_Record.Helpers;
using Electronic_Medical_Record.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RandomGen;

namespace Electronic_Medical_Record.Repositories
{
    public class DoctorsRepository : BaseRepository<Doctor>
    {
        public DoctorsRepository(string conn) : base (conn)
        {
        }

        public override void SetDefaultData()
        {
            string sqlExpression = $@"INSERT INTO [dbo].[{nameof(Doctor).MakePlular()}](
                                                         [{nameof(Doctor.FirstName)}]
                                                        ,[{nameof(Doctor.LastName)}]
                                                        ,[{nameof(Doctor.Position)}]
                                                        ,[{nameof(Doctor.PhoneNumber)}]
                                                        ,[{nameof(Doctor.Birthday)}])
                                                    VALUES";
            int dataCount = 20;
            for (int i = 1; i <= dataCount; i++)
            {
                string name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()();
                sqlExpression += "('" + name +
                    "','" + Gen.Random.Names.Surname()() +
                    "','" + Gen.Random.Text.Words()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.PhoneNumbers.WithRandomFormat()() +
                    "','" + Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)() +
                    "')";
                if (i != dataCount)
                {
                    sqlExpression += ",";
                }

            }
            SetDefaultData(sqlExpression);
        }

        protected override Doctor Map(SqlDataReader reader)
        {
            var model = new Doctor();

            model.Id = (int)reader[nameof(model.Id)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];
            model.Position = (string)reader[nameof(model.Position)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.Birthday = (DateTime)reader[nameof(model.Birthday)];

            return model;
        }
        /*
        public IEnumerable<Doctor> GetAll()
        {
            List<Doctor> Records = new List<Doctor>();

            string sqlExpression = "SELECT * FROM Records";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Doctor record = new Doctor();
                        Map(reader, record);
                        Records.Add(record);
                    }
                }
                connection.Close();
            }
            return Records;
        }

        public Doctor FindById(int id)
        {
            Doctor model = new Doctor();
            string sqlExpression = @"SELECT * FROM Doctors" +
                                     " WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                        Map(reader, model);
                }
                connection.Close();
            }
            return model;
        }

        public void Create(Doctor model)
        {
            string sqlExpression = @"INSERT INTO Records ([FirstName]" +
                                                        ",[LastName]" +
                                                        ",[Position]" +
                                                        ",[PhoneNumber]" +
                                                        ",[Birthday])" +
                                                 "VALUES (@FirstName" +
                                                        ",@LastName" +
                                                        ",@Position" +
                                                        ",@PhoneNumber" +
                                                        ",@Birthday)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@FirstName", model.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", model.LastName));
                command.Parameters.Add(new SqlParameter("@Position", model.Position));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Birthday", model.Birthday));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Update(Doctor model)
        {
            string sqlExpression = @"UPDATE Records" +
                                    " SET[FirstName] = @FirstName" +
                                       ",[LastName] = @LastName" +
                                       ",[Position] = @Position" +
                                       ",[PhoneNumber] = @PhoneNumber" +
                                       ",[Birthday] = @Birthday" +
                                   " WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", model.Id));
                command.Parameters.Add(new SqlParameter("@FirstName", model.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", model.LastName));
                command.Parameters.Add(new SqlParameter("@Position", model.Position));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", model.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@Birthday", model.Birthday));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = @"DELETE FROM Records" +
                                   " WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void Map(SqlDataReader reader, Doctor model)
        {
            model.Id = (int)reader[nameof(model.Id)];
            model.FirstName = (string)reader[nameof(model.FirstName)];
            model.LastName = (string)reader[nameof(model.LastName)];
            model.Position = (string)reader[nameof(model.Position)];
            model.PhoneNumber = (string)reader[nameof(model.PhoneNumber)];
            model.Birthday = (DateTime)reader[nameof(model.Birthday)];
        }

        public void CheckTable()
        {
            if(!IsTableExist())
            {
                CreateTable();
                SetDefaultData();
            }
        }

        public bool IsTableExist()
        {
            string sqlExpression = @"IF OBJECT_ID('dbo." + nameof(Doctor).MakePlular() + "', 'U') IS NOT NULL " +
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

        public void CreateTable()
        {
            string sqlExpression = @"CREATE TABLE [dbo].[Doctors](
                                        [Id] [int] IDENTITY(1,1) NOT NULL,
                                        [FirstName] [nvarchar](60) NOT NULL,
                                        [LastName] [nvarchar](60) NOT NULL,
                                        [Birthday] [datetime] NOT NULL,
                                        [PhoneNumber] [nvarchar](60) NOT NULL,
                                        [Position] [nvarchar](60) NOT NULL,
                                        CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
                                        (
                                            [Id] ASC
                                        )
                                        )";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();

                SetDefaultData();
            }
        }

        public void SetDefaultData()
        {
            string sqlExpression = @"INSERT INTO [dbo].[Doctors](
                                                         [FirstName]
                                                        ,[LastName]
                                                        ,[Position]
                                                        ,[PhoneNumber]
                                                        ,[Birthday])
                                                    VALUES";
            int dataCount = 20;
            for (int i = 1; i <= dataCount; i++)
            {
                string name = i % 2 == 0 ? Gen.Random.Names.Male()() : Gen.Random.Names.Female()();
                sqlExpression += "('" + name +
                    "','" + Gen.Random.Names.Surname()() +
                    "','" + Gen.Random.Text.Words()().MakeFirstCharUppercase() +
                    "','" + Gen.Random.PhoneNumbers.WithRandomFormat()() +
                    "','" + Gen.Random.Time.Dates(DateTime.Now.AddYears(-100), DateTime.Now)() +
                    "')";
                if (i != dataCount)
                {
                    sqlExpression += ",";
                }

            }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }*/
    }
}
