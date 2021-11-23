using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class Record : BaseModel
    {
        public Record()
        {
            Diagnosis = new Diagnosis();
            SickLeave = new SickLeave();
            Doctor = new Doctor();
            Patient = new Patient();
        }
        public int DiagnosisId { get; set; }
        public int SickLeaveId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Diagnosis Diagnosis { get; set; }
        public SickLeave SickLeave { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }


    }
}
