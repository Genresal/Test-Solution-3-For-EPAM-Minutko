using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.DataTables
{
    /// <summary>
    /// A jQuery DataTables column. https://datatables.net/manual/server-side
    /// </summary>
    public class DataTablesColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DataTablesSearch Search { get; set; }
    }
}
