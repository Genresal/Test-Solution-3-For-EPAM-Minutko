using EMR.Business.Models;
using EMR.ViewModels;
using System.Collections.Generic;

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
