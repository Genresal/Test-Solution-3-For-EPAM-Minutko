using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class DrugPageService : BasePageService<Drug, DrugViewModel>, IDrugPageService
    {
        private readonly IDrugService _drugService;
        public DrugPageService(IDrugService drugService, IMapper mapper) : base(drugService, mapper)
        {
            _drugService = drugService;
        }

        public IEnumerable<DrugViewModel> LoadTable(DrugSearchModel searchParameters)
        {
            var rawResult = _drugService.GetDrugsForRecord(searchParameters.RecordId);

            var result = _mapper.Map<IEnumerable<Drug>, IEnumerable<DrugViewModel>>(rawResult);

            var searchBy = searchParameters.Search?.Value;

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }
    }
}
