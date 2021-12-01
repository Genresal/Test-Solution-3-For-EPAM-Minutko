using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class UserService : BaseBusinessService<User>
    {
        public UserService(IRepository<User> r) : base (r)
        {
        }
    }
}
