using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class ProcedureService : BaseBusinessService<Procedure>
    {
        public ProcedureService(IRepository<Procedure> r) : base (r)
        {
        }
    }
}
