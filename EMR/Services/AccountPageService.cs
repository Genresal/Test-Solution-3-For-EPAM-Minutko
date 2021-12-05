using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMR.Services
{
    public class AccountPageService :  IAccountPageService
    {
        readonly IBusinessService<User> _pageService;
        readonly IBusinessService<Role> _roleService;
        private readonly IMapper _mapper;

        public AccountPageService(IBusinessService<User> userService, IBusinessService<Role> roleService, IMapper mapper)
        {
            _pageService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        public bool IsLoginExist(string login)
        {
            return _pageService.GetByColumn(nameof(User.Login), login).Any();
        }
    }
}
