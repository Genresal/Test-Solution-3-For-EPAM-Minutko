using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class DrugViewModel
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        [Required(ErrorMessage = "Add a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Add a description")]
        public string Description { get; set; }
    }
}
