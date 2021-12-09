using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int DiagnosisId { get; set; }
        public int SickLeaveId { get; set; }
        [Required]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }
        [Display(Name = "Diagnosis")]
        public string DiagnosisWithCode { get; set; }
        [Display(Name = "Patient name")]
        public string PatientName { get; set; }
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }
        [Display(Name = "Doctor's position")]
        public string DoctorPosition { get; set; }
        public int DoctorPositionId { get; set; }
        [Display(Name = "Modified date")]
        public DateTime ModifiedDate { get; set; }
    }
}
