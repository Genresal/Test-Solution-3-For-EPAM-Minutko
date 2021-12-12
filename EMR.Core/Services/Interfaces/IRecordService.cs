using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface IRecordService : IBusinessService<Record>
    {
        public Record GetLast();
    }


}
