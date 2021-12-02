using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class UserService : BaseBusinessService<User>, IBusinessService<User>
    {
        public UserService(IRepository<User> r) : base (r)
        {
        }
    }
}
