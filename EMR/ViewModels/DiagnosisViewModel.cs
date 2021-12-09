using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class DiagnosisViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Diagnosis name")]
        public string Name { get; set; }
    }
}
