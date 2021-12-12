using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface IPositionService : IBusinessService<Position>
    {
        public bool IsPositionInUse(int positionId);
    }


}
