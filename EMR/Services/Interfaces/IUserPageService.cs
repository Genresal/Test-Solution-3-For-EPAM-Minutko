﻿using EMR.Business.Models;
using EMR.Business.Services;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public interface IUserPageService : IBasePageService<UserViewModel>
    {
        public IEnumerable<UserViewModel> LoadTable(UserSearchModel searchParameters);
        public IEnumerable<Role> GetRoles();
        public UserViewModel GetByLogin(string login);
        public DoctorViewModel GeDoctorByUserId(int userId);
        public PatientViewModel GePatientByUserId(int userId);
        public UserViewModel GetRandomAccount(int roleId);
        public bool ChangePassword(ChangePasswordViewModel password, string login);
        public UserViewModel LogIn(LoginViewModel model);
    }
}
