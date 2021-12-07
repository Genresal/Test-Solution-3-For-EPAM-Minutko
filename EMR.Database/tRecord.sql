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
      REFERENCES [tDiagnosis]([Id])
      ON DELETE CASCADE,
);