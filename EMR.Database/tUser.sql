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
