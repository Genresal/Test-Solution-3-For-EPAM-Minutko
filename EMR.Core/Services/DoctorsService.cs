using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;

namespace EMR.Business.Services
{
    public class DoctorService : BaseBusinessService<Doctor>, IDoctorService
    {
        public DoctorService(IRepository<Doctor> doctorRepository, ILogger<DoctorService> logger) : base(doctorRepository, logger)
        {
        }
    }
}
