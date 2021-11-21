using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.DataTables
{
    /// <summary>
    /// Sort orders of jQuery DataTables. https://datatables.net/manual/server-side
    /// </summary>
    public enum TableOrder
    {
        Asc,
        Desc,
    }

    public class DataTablesOrder
    {
        public int Column { get; set; }
        public TableOrder Dir { get; set; }
    }
}
