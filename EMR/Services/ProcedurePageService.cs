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
        private readonly ITreatmentService _treatmentService;

        public ProcedurePageService(IProcedureService procedureService, ITreatmentService treatmentService, IMapper mapper) : base(procedureService, mapper)
        {
            _procedureService = procedureService;
            _treatmentService = treatmentService;
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

        public int GetRecordId(int procedureId)
        {
            return _treatmentService.GetDrugRecordId(procedureId);
        }
    }
}
