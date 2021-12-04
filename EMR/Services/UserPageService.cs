using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class UserPageService : BasePageService<User, UserViewModel>, IUserPageService
    {
        readonly IBusinessService<User> _pageService;

        public UserPageService(IBusinessService<User> userService, IMapper mapper) : base(userService, mapper)
        {
            _pageService = userService;
        }

        public IEnumerable<UserViewModel> LoadTable(UserSearchModel searchParameters)
        {
            IEnumerable<User> rawResult = _pageService.GetAll();

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(rawResult);

            if (!string.IsNullOrEmpty(searchParameters.FullName))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchParameters.FullName.ToUpper()));
            }

            var searchBy = searchParameters.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public bool IsLoginExist(string login)
        {
            return _pageService.GetByColumn(nameof(User.Login), login).Any();
        }

        public User GetUserByLogin(string login)
        {
            return _pageService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
        }
    }
}
