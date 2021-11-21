using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.DataTables
{
    /// <summary>
    /// Datatables sent parameters. https://datatables.net/manual/server-side
    /// </summary>
    public class DataTablesParameters
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTablesSearch Search { get; set; }
        public DataTablesColumn[] Columns { get; set; }
        public DataTablesOrder[] Order { get; set; }
    }
}
