using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
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
