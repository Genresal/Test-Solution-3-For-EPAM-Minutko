using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Mapper;
using EMR.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using EMR.Helpers;
using AutoMapper;

namespace EMR.Services
{
    public class ProcedurePageService : BasePageService<Procedure>, IProcedurePageService
    {
        private readonly IProcedureService _procedureService;
        private readonly IMapper _mapper;

        public ProcedurePageService(IProcedureService procedureService, IMapper mapper) : base(procedureService)
        {
            _procedureService = procedureService;
            _mapper = mapper;
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
