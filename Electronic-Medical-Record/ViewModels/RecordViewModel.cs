using Electronic_Medical_Record.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.ViewModels
{
    public class RecordViewModel : Record
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}
