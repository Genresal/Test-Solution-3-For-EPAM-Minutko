using EMR.DataTables;

namespace EMR.ViewModels
{
    public class ProcedureSearchModel : DataTablesParameters
    {
        public int RecordId { get; set; }
        public bool isUserAllowedToEdit { get; set; }
    }
}
