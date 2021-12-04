using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class ProcedurePageService : BasePageService<Procedure, ProcedureViewModel>, IProcedurePageService
    {
        private readonly IProcedureService _procedureService;

        public ProcedurePageService(IProcedureService procedureService, IMapper mapper) : base(procedureService, mapper)
        {
            _procedureService = procedureService;
        }

        public IEnumerable<ProcedureViewModel> LoadTable(ProcedureSearchModel searchParameters)
        {
            var rawResult = _procedureService.GetProceduresForRecord(searchParameters.RecordId);

            var result = _mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureViewModel>>(rawResult);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }
    }
}
