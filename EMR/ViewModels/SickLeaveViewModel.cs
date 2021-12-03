using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class SickLeaveViewModel : DataTablesParameters
    {
        public SickLeaveViewModel()
        {
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
