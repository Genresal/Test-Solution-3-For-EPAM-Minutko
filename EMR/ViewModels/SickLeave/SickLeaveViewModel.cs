﻿using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class SickLeaveViewModel : DataTablesParameters
    {
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