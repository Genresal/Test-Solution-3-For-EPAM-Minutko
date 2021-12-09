using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class PatientInfoViewModel
    {
        public int PatientId { get; set; }
        [Display(Name = "Records number")]
        public int RecordsNumber { get; set; }
        [Display(Name = "Last record date")]
        public DateTime LastRecordModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Patient's name")]
        public string FullName { get; set; }
    }
}
