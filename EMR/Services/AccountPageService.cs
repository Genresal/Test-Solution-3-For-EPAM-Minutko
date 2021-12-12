using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Helpers;
using EMR.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMR.Services
{
    public class AccountPageService :  IAccountPageService
    {
        private readonly IUserService _userService;
        private readonly IBusinessService<Role> _roleService;
        private readonly IMapper _mapper;

        public AccountPageService(IUserService userService, IBusinessService<Role> roleService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        public bool IsLoginExist(string login)
        {
            return _userService.GetByColumn(nameof(User.Login), login).Any();
        }

        public List<SelectListItem> PrepareRoles()
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            roles.AddRange(_roleService.GetAll()
                    .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList());

            return roles;
        }
    }
}
