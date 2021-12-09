using EMR.ViewModels;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IPositionPageService : IBasePageService<PositionViewModel>
    {
        public IEnumerable<PositionViewModel> LoadTable(PositionSearchModel searchParameters);
        public bool IsPositionInUse(int positionId);
    }
}
