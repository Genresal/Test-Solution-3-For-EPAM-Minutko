﻿
CREATE TABLE [dbo].[tSickLeave](
  [Id] [int] IDENTITY(1,1) NOT NULL,
  [Number] [nvarchar](255) NOT NULL,
  [StartDate] [datetime] NOT NULL,
  [FinalDate] [datetime] NOT NULL,
  PRIMARY KEY ([Id]),
  );