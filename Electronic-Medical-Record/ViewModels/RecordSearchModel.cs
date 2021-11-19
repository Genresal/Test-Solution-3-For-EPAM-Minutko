using Electronic_Medical_Record.Helpers.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.ViewModels
{
    public class RecordSearchModel : DataTablesParameters
    {
        public RecordSearchModel()
        {
            DateRange = new FilterRange(typeof(DateTime));
        }

        [Display(Name = "Date range, from-to")]
        public FilterRange DateRange { get; set; }
    }
}
