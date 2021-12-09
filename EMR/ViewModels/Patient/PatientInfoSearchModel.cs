using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

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
