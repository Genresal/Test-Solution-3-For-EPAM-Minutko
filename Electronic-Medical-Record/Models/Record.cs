using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.Models
{
    public class Record
    {
        public int Id { get; set; }
        [Display(Name = "Patient")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a patient")]
        public int PatientId { get; set; }
        [Display(Name = "Diagnosis")]
        [Required]
        public string Diagnosis { get; set; }
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public DateTime ModifyingDate { get; set; }
    }
}
