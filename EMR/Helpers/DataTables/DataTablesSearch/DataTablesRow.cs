using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMR.DataTables
{
    /// <summary>
    /// jQuery DataTables row. https://datatables.net/manual/server-side
    /// </summary>
    public abstract class DataTablesRow
    {
        [JsonPropertyName("DT_RowId")]
        public virtual string DtRowId => null;
        [JsonPropertyName("DT_RowClass")]
        public virtual string DtRowClass => null;
        [JsonPropertyName("DT_RowData")]
        public virtual object DtRowData => null;
        [JsonPropertyName("DT_RowAttr")]
        public virtual object DtRowAttr => null;
    }
}
