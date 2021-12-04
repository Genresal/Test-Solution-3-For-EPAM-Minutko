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
using AutoMapper;
using EMR.Helpers;

namespace EMR.Services
{
    public class DrugPageService : BasePageService<Drug>, IDrugPageService
    {
        private readonly IDrugService _drugService;
        private readonly IMapper _mapper;
        public DrugPageService(IDrugService drugService, IMapper mapper) : base(drugService)
        {
            _drugService = drugService;
            _mapper = mapper;
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
