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