using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
