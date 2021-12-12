using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;

namespace EMR.Services
{
    public class SickLeavePageService : BasePageService<SickLeave, SickLeaveViewModel>, ISickLeavePageService
    {
        private readonly ISickLeaveService _sickLeaveService;
        public SickLeavePageService(ISickLeaveService service, IMapper mapper) : base(service, mapper)
        {
            _sickLeaveService = service;
        }

        public override void Create(SickLeaveViewModel model)
        {
            _sickLeaveService.Create(_mapper.Map<SickLeaveViewModel, SickLeave>(model), model.RecordId);
        }
    }
}
