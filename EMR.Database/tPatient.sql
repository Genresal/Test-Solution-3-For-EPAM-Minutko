CREATE TABLE [dbo].[tPatient](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [UserId] [int] NOT NULL,
  [Job] [nvarchar](255) NOT NULL,
  PRIMARY KEY ([Id]),
        FOREIGN KEY ([UserId])
      REFERENCES [tUser]([Id])
	  ON DELETE CASCADE
  );