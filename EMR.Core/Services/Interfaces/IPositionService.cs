using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface IPositionService : IBusinessService<Position>
    {
        public bool IsPositionInUse(int positionId);
    }


}
