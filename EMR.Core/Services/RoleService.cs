using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;

namespace EMR.Business.Services
{
    public class RoleService : BaseBusinessService<Role>, IBusinessService<Role>
    {
        public RoleService(IRepository<Role> repository, ILogger<RoleService> logger) : base(repository, logger)
        {
        }
    }
}
