using ERM.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERM.ViewModels
{
    public class TreatmentSearchModel : DataTablesParameters
    {
        public TreatmentSearchModel()
        {
            DateRange = new FilterRange(typeof(DateTime));
        }

        [Display(Name = "Date range, from-to")]
        public FilterRange DateRange { get; set; }

        public int RecordId { get; set; }
    }
}
