using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string Drug { get; set; }
        public string DrugUsage { get; set; }
        public string Procedure { get; set; }
        public DateTime ModifyingDate { get; set; }
    }
}
