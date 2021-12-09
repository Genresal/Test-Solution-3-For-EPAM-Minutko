using AutoMapper;
using EMR.Business.Models;
using EMR.Business.Services;
using EMR.Business.Helpers;
using EMR.Helpers;
using EMR.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Services
{
    public class UserPageService : BasePageService<User, UserViewModel>, IUserPageService
    {
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IBusinessService<Role> _roleService;

        public UserPageService(IUserService userService
            , IBusinessService<Role> roleService
            , IDoctorService doctorService
            , IPatientService patientService
            , IMapper mapper) : base(userService, mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public IEnumerable<UserViewModel> LoadTable(UserSearchModel searchParameters)
        {
            IEnumerable<User> rawResult = _userService.GetAll();

            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(rawResult);

            if (!string.IsNullOrEmpty(searchParameters.FullName))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchParameters.FullName.ToUpper()));
            }

            if (searchParameters.Roles.Count > 0)
            {
                var filterParams = searchParameters.Roles.Select(r => r.Id);
                result = result.Where(r => filterParams.Contains(r.RoleId));
            }

            var searchBy = searchParameters.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.FullName != null && r.FullName.ToString().ToUpper().Contains(searchBy.ToUpper()));
            }

            return result.Order(searchParameters);
        }

        public override void Create(UserViewModel viewModel)
        {
            var model = _mapper.Map<UserViewModel, User>(viewModel);
            model.Password = model.Password.HashString();
            _mainService.Create(model);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        public UserViewModel GetRandomAccount(int roleId)
        {
            var rawResult = _userService.GetRandomAccount(roleId);
            return _mapper.Map<UserViewModel>(rawResult);
        }

        public bool IsLoginExist(string login)
        {
            return _userService.GetByColumn(nameof(User.Login), login).Any();
        }

        public UserViewModel GetByLogin(string login)
        {
            var rawResult = _userService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
            return _mapper.Map<UserViewModel>(rawResult);
        }

        public UserViewModel LogIn(LoginViewModel model)
        {
            var rawResult = _userService.GetByColumn(nameof(User.Login), model.Login)
                .FirstOrDefault(x => x.Password == model.Password.HashString());
            return _mapper.Map<UserViewModel>(rawResult);
        }

        public DoctorViewModel GeDoctorByUserId(int userId)
        {
            var rawResult = _doctorService.GetByColumn(nameof(Doctor.UserId), userId.ToString()).FirstOrDefault();
            return _mapper.Map<DoctorViewModel>(rawResult);
        }

        public PatientViewModel GePatientByUserId(int userId)
        {
            var rawResult = _patientService.GetByColumn(nameof(Patient.UserId), userId.ToString()).FirstOrDefault();
            return _mapper.Map<PatientViewModel>(rawResult);
        }

        public bool ChangePassword(ChangePasswordViewModel password, string login)
        {
            var user = GetByLogin(login);
            if(user.Password == password.OldPassword.HashString())
            {
                _userService.ChangePassword(user.Id, password.NewPassword.HashString());
                return true;
            }
            return false;
        }
    }
}
