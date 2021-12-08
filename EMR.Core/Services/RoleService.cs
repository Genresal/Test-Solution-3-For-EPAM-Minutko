﻿using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class RoleService : BaseBusinessService<Role>, IBusinessService<Role>
    {
        public RoleService(IRepository<Role> repository, ILogger<RoleService> logger) : base (repository, logger)
        {
        }
    }
}
