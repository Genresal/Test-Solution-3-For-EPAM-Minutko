using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class PositionPageService : BasePageService<Position, PositionViewModel>, IPositionPageService
    {
        private readonly IPositionService _positionService;

        public PositionPageService(IPositionService positionService, IMapper mapper) : base(positionService, mapper)
        {
            _positionService = positionService;
        }

        public IEnumerable<PositionViewModel> LoadTable(PositionSearchModel searchParameters)
        {
            var rawResult = _positionService.GetAll();

            var result = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionViewModel>>(rawResult);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public bool IsPositionInUse(int positionId)
        {
            return _positionService.IsPositionInUse(positionId);
        }
    }
}
