using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface ISickLeaveService : IBusinessService<SickLeave>
    {
        public void Create(SickLeave model, int recordId);
    }
}
