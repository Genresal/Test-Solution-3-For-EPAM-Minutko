using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class ProcedureSearchModel : DataTablesParameters
    {
        public ProcedureSearchModel()
        {
        }

        public int RecordId { get; set; }
    }
}
