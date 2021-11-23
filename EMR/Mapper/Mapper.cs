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
                    PatientName = $"{record.Patient.User.FirstName} {record.Patient.User.FirstName}",
                    DoctorName = $"Dr. {record.Doctor.User.FirstName} {record.Doctor.User.LastName}",
                    ModifiedDate = record.ModifiedDate
                };
            }
            return null;
        }
    }
}
