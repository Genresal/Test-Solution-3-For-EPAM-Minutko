using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Business.Models
{
    public class DbStatus
    {
        public bool IsTablesExist { get; set; }
        public bool IsDataExist { get; set; }
    }
}
