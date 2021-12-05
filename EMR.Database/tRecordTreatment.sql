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
  );
