using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PositionService : BaseBusinessService<Position>
    {
        public PositionService(IRepository<Position> r) : base (r)
        {
        }
    }
}
