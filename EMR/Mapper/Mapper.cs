using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Mapper
{
    public static class Mapper
    {
        public static RecordViewModel ToViewModel(this Record record)
        {
            if (record != null)
            {
                return new RecordViewModel
                {
                    Id = record.Id,
                    Diagnosis = $"({record.DiagnosisId.ToString("000")}) {record.Diagnosis.Name}",
                    PatientName = $"{record.Patient.User.FirstName} {record.Patient.User.LastName}",
                    Doctor = $"Dr. {record.Doctor.User.FirstName} {record.Doctor.User.LastName}",
                    DoctorPosition = record.Doctor.Position.Name,
                    DoctorPositionId = record.Doctor.PositionId,
                    ModifiedDate = record.ModifiedDate
                };
            }
            return null;
        }
    }
}
