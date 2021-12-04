using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;

namespace EMR.Services
{
    public abstract class BasePageService<T, V>  where T : BaseModel
    {
        protected readonly IBusinessService<T> _mainService;
        protected readonly IMapper _mapper;

        protected BasePageService(IBusinessService<T> mainService, IMapper mapper)
        {
            _mainService = mainService;
            _mapper = mapper;
        }

        public virtual V GetById(int id)
        {
            var rawResult = _mainService.GetById(id);
            return _mapper.Map<T, V>(rawResult);
        }

        public virtual void Create(V viewModel)
        {
            var model = _mapper.Map<V, T>(viewModel);
            _mainService.Create(model);
        }
        public virtual void Update(V viewModel)
        {
            var model = _mapper.Map<V, T>(viewModel);
            _mainService.Update(model);
        }
        public virtual void Delete(int id)
        {
            _mainService.Delete(id);
        }
    }
}
