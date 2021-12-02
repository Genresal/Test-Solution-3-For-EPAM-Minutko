using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class PatientSearchModel : DataTablesParameters
    {
        public PatientSearchModel()
        {
        }

        public int DoctorId { get; set; }
    }
}
