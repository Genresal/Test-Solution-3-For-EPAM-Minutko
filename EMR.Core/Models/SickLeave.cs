using System;

namespace EMR.Business.Models
{
    public class SickLeave : BaseModel
    {
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
    }
}
