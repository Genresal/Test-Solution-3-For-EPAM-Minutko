using ERM.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.ViewModels
{
    public class SickLeaveSearchModel : DataTablesParameters
    {
        public SickLeaveSearchModel()
        {
            DateRange = new FilterRange(typeof(DateTime));
        }

        [Display(Name = "Date range, from-to")]
        public FilterRange DateRange { get; set; }

        public int RecordId { get; set; }
    }
}
