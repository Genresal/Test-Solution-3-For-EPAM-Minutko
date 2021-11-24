﻿using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        {
            string sqlExpression = $@"USE [master]
                                    GO
                                    
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
            string sqlExpression = $@"USE [master]
                                    GO
                                    
                                    IF  NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'EMR')
                                    CREATE DATABASE [EMR];

                                    USE [EMR]
                                    GO";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void CreateTables()
        {
            string sqlExpression = $@"USE [EMR]
GO

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
/*
 Treatment
*/
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

/*
 Users section
*/
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
  [PhoneNumber] [nvarchar](255) NOT NULL,
  [PhotoUrl] [nvarchar](255) NOT NULL,
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
  [DrugId] [int] NOT NULL,
  [ProcedureId] [int] NOT NULL,
  PRIMARY KEY ([Id]),
        FOREIGN KEY ([RecordId])
      REFERENCES [tRecord]([Id]),
	        FOREIGN KEY ([DrugId])
      REFERENCES [tDrug]([Id]),
	        FOREIGN KEY ([ProcedureId])
      REFERENCES [tProcedure]([Id])
  );";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void DropTables()
        {
            string sqlExpression = $@"USE [EMR]
GO

DROP TABLE [tRecordTreatment];
DROP TABLE [tRecord];
DROP TABLE [tPatient];
DROP TABLE [tDoctor];
DROP TABLE [tPosition];
DROP TABLE [tUser];
DROP TABLE [tRole];
DROP TABLE [tProcedure];
DROP TABLE [tDrug];
DROP TABLE [tDiagnosis];
DROP TABLE [tSickLeave];";
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