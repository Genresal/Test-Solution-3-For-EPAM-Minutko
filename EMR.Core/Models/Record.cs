using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class Record : BaseModel
    {
        public int DiagnosisId { get; set; }
        public int SickLeaveId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ModifiedDate { get; set; }


    }
}
