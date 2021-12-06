using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class PositionService : BaseBusinessService<Position>, IPositionService
    {
        private readonly IRepository<Doctor> _doctorRepository;
        public PositionService(IRepository<Position> positionRepository, IRepository<Doctor> doctorRepository) : base (positionRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public bool IsPositionInUse(int positionId)
        {
            return _doctorRepository.GetByColumn(nameof(Doctor.PositionId), positionId).Any();
        }
    }
}
