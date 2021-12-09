﻿using EMR.Business.Models;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    public interface IPositionService : IBusinessService<Position>
    {
        public bool IsPositionInUse(int positionId);
    }


}
