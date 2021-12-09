namespace EMR.Business.Models
{
    public class RecordTreatment : BaseModel
    {
        public int RecordId { get; set; }
        public int? DrugId { get; set; }
        public int? ProcedureId { get; set; }
    }
}
