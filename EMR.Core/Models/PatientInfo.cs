using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class PatientInfo : BaseModel
    {
        public int PatientId { get; set; }
        public int RecordsNumber { get; set; }
        public DateTime LastRecordModified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
