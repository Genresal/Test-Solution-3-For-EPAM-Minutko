CREATE TABLE [dbo].[tDrug](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Name] [nvarchar](255) NOT NULL,
  [Description] [nvarchar](1000) NOT NULL,
  PRIMARY KEY ([Id]),
  );