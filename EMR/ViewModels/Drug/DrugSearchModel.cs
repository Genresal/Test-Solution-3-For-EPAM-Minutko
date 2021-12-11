using EMR.DataTables;

namespace EMR.ViewModels
{
    public class DrugSearchModel : DataTablesParameters
    {
        public int RecordId { get; set; }
        public bool isUserAllowedToEdit { get; set; }
    }
}
