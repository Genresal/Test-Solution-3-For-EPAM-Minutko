using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class PatientInfoSearchModel : DataTablesParameters
    {
        public PatientInfoSearchModel()
        {
            DateRange = new FilterRange(typeof(DateTime));
        }
        [Display(Name = "Date range, from-to")]
        public FilterRange DateRange { get; set; }
        public int DoctorId { get; set; }
    }
}
