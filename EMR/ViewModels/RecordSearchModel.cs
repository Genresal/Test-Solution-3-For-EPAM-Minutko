using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class RecordSearchModel : DataTablesParameters
    {
        public RecordSearchModel()
        {
            DateRange = new FilterRange(typeof(DateTime));
            DoctorPositions = new List<FilterCondition>();
        }
        [Display(Name = "Search by diagnosis")]
        public string Diagnosis { get; set; }

        [Display(Name = "Doctor positions")]
        public List<FilterCondition> DoctorPositions { get; set; }

        [Display(Name = "Date range, from-to")]
        public FilterRange DateRange { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
