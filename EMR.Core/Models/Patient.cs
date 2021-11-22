using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class Patient : BaseModel
    {
        public int UserId { get; set; }
        public string Job { get; set; }
    }
}
