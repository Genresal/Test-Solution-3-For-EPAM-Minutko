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

namespace EMR.Services
{
    public class UserPageService : IUserPageService
    {
        readonly IBusinessService<User> _service;

        public UserPageService(IBusinessService<User> s)
        {
            _service = s;
        }

        public bool IsLoginExist(string login)
        {
            return _service.GetByColumn(nameof(User.Login), login).Any();
        }

        public User GetUserByLogin(string login)
        {
            return _service.GetByColumn(nameof(User.Login), login).FirstOrDefault();
        }

        public User GetById(int id)
        {
            return _service.GetById(id);
        }
        public void Create(User item)
        {
            _service.Create(item);
        }
        public void Update(User item)
        {
            _service.Update(item);
        }
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
