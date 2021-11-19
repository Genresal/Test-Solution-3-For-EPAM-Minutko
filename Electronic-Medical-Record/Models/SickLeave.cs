using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.Models
{
    public class SickLeave
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int FormId { get; set; }
        public int DiagnosisCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
