using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EMR.Business.Services
{
    public class PositionService : BaseBusinessService<Position>, IPositionService
    {
        private readonly IRepository<Doctor> _doctorRepository;
        public PositionService(IRepository<Position> positionRepository
            , IRepository<Doctor> doctorRepository
            , ILogger<PositionService> logger) : base(positionRepository, logger)
        {
            _doctorRepository = doctorRepository;
        }
        public bool IsPositionInUse(int positionId)
        {
            return _doctorRepository.GetByColumn(nameof(Doctor.PositionId), positionId).Any();
        }
    }
}
