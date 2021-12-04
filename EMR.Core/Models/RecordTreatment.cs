using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class RecordTreatment : BaseModel
    {
        public int RecordId { get; set; }
        public int? DrugId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
