using System;
using System.ComponentModel.DataAnnotations;

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
