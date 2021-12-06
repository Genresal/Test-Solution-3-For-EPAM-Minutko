using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace EMR.Data.Repositories
{
    public class DbRepository : IDbRepository
    {
        protected string connectionString = null;
        public DbRepository(string conn)
        {
            connectionString = conn;
        }

        public void DropDb()
        {//USE [master] GO
            string sqlExpression = $@"
                                    
                                    ALTER DATABASE [EMR] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                                    IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'EMR')
                                    DROP DATABASE [EMR];";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void CreateDb()
        {
            string sqlExpression = $@"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'EMR')
                                    CREATE DATABASE [EMR];";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public bool IsDbExist()
        {
            string sqlExpression = $@"IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'EMR')
                                                                                        SELECT 1 as 'Result' 
                                                                                        ELSE 
                                                                                        SELECT 0 as 'Result'";
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

        //File.ReadAllText(@"C:\Users\genre\source\repos\Test-Solution-3-For-EPAM-Minutko\EMR.Database\bin\Debug\EMR.Database_1.publish.sql").Replace("GO", "");

        public void CreateTables()
        {//USE [EMR] GO
            string sqlExpression = $@" USE [EMR] 
CREATE TABLE [dbo].[tSickLeave](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Number] [nvarchar](255) NOT NULL,
  [StartDate] [datetime] NOT NULL,
  [FinalDate] [datetime] NOT NULL,
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tDiagnosis](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tDrug](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  [Description] [nvarchar](1000) NOT NULL,
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tProcedure](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  [Description] [nvarchar](1000) NOT NULL,
  PRIMARY KEY ([Id]),
  );


CREATE TABLE [dbo].[tRole](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tUser](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Login] [nvarchar](255) NOT NULL,
  [Password] [nvarchar](255) NOT NULL,
  [RoleId] [int] NOT NULL,
  [FirstName] [nvarchar](255) NOT NULL,
  [LastName] [nvarchar](255) NOT NULL,
  [Birthday] [datetime] NOT NULL,
  [Email] [nvarchar](255) NOT NULL,
  [PhoneNumber] [nvarchar](255) NOT NULL,
  [PhotoUrl] [nvarchar](255),
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tPosition](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  PRIMARY KEY ([Id]),
  );

CREATE TABLE [dbo].[tDoctor](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [UserId] [int] NOT NULL,
  [PositionId] [int] NOT NULL,
  PRIMARY KEY ([Id]),
      FOREIGN KEY ([UserId])
      REFERENCES [tUser]([Id])
	  ON DELETE CASCADE,
	  FOREIGN KEY ([PositionId])
      REFERENCES [tPosition]([Id])
  );

CREATE TABLE [dbo].[tPatient](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [UserId] [int] NOT NULL,
  [Job] [nvarchar](255) NOT NULL,
  PRIMARY KEY ([Id]),
        FOREIGN KEY ([UserId])
      REFERENCES [tUser]([Id])
	  ON DELETE CASCADE
  );

CREATE TABLE [dbo].[tRecord](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [DiagnosisId] [int] NOT NULL,
  [SickLeaveId] [int] NOT NULL,
  [DoctorId] [int] NOT NULL,
  [PatientId] [int] NOT NULL,
  [ModifiedDate] [datetime] NOT NULL,
  PRIMARY KEY ([Id]),
    FOREIGN KEY ([DoctorId])
      REFERENCES [tDoctor]([Id]),
	FOREIGN KEY ([PatientId])
      REFERENCES [tPatient]([Id]),
    FOREIGN KEY ([SickLeaveId])
	  REFERENCES [tSickLeave]([Id]),
    FOREIGN KEY ([DiagnosisId])
      REFERENCES [tDiagnosis]([Id]),
);

CREATE TABLE [dbo].[tRecordTreatment](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [RecordId] [int] NOT NULL,
  [DrugId] [int],
  [ProcedureId] [int],
  PRIMARY KEY ([Id]),
        FOREIGN KEY ([RecordId])
      REFERENCES [tRecord]([Id]) 
ON DELETE CASCADE,
	        FOREIGN KEY ([DrugId])
      REFERENCES [tDrug]([Id]) 
ON DELETE CASCADE,
	        FOREIGN KEY ([ProcedureId])
      REFERENCES [tProcedure]([Id])
ON DELETE CASCADE
  );";

                



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