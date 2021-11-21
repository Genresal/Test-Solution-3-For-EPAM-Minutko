using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ERM.DataTables
{
    /// <summary>
    /// jQuery DataTables search field. https://datatables.net/manual/server-side
    /// </summary>
    public class DataTablesSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
}
