using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class SickLeaveService : BaseBusinessService<SickLeave>, ISickLeaveService
    {
        public SickLeaveService(IRepository<SickLeave> repository, ILogger<UserService> _logger) : base (repository, _logger)
        {
        }

        public void Create(SickLeave model, int recordId)
        {
            _mainRepository.Create(model, recordId);
        }
    }
}
