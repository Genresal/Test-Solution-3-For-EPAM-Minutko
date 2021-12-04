using EMR.Business.Services;

namespace EMR.Services
{
    public abstract class BasePageService<T>
    {
        protected readonly IBusinessService<T> _mainService;

        protected BasePageService(IBusinessService<T> mainService)
        {
            _mainService = mainService;
        }

        public virtual T GetById(int id)
        {
            return _mainService.GetById(id);
        }

        public virtual void Create(T item)
        {
            _mainService.Create(item);
        }
        public virtual void Update(T item)
        {
            _mainService.Update(item);
        }
        public virtual void Delete(int id)
        {
            _mainService.Delete(id);
        }
    }
}
