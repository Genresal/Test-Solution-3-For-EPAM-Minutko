using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
