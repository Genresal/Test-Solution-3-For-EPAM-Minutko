using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
