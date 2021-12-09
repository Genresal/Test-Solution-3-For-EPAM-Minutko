using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class DrugViewModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        [Required(ErrorMessage = "Add a name")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Add a description")]
        [Display(Name = "Desccription")]
        public string Description { get; set; }
    }
}
