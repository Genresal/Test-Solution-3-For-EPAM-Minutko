using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string Name { get; set; }
    }
}
