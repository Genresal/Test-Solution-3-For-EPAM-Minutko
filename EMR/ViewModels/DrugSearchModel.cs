using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class DrugSearchModel : DataTablesParameters
    {
        public DrugSearchModel()
        {
        }

        public int RecordId { get; set; }
    }
}
