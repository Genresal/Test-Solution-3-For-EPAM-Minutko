using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EMR.DataTables
{
    /// <summary>
    /// Result for jQuery DataTables. https://datatables.net/manual/server-side
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesResult<T>
    {
        [JsonPropertyName("draw")]
        public int Draw { get; set; }
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
