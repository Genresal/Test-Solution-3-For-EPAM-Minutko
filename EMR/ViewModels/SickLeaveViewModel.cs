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
        public int RecordId { get; set; }
        public string Number { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Final date")]
        public DateTime FinalDate { get; set; }
    }
}
