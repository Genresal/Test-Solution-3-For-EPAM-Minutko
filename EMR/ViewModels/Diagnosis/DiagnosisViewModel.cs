using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class DiagnosisViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Diagnosis name")]
        public string Name { get; set; }
    }
}
