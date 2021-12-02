using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class DrugService : BaseBusinessService<Drug>, IBusinessService<Drug>
    {
        public DrugService(IRepository<Drug> r) : base (r)
        {
        }
    }
}