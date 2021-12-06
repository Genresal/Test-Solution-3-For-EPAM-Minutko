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
        readonly IDoctorService _doctorService;
        readonly IPatientService _patientService;
        readonly IBusinessService<Role> _roleService;

        public UserPageService(IBusinessService<User> userService
            , IBusinessService<Role> roleService
            , IDoctorService doctorService
            , IPatientService patientService
            , IMapper mapper) : base(userService, mapper)
        {
            _pageService = userService;
            _roleService = roleService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        public IEnumerable<UserViewModel> LoadTable(UserSearchModel searchParameters)
        {
            IEnumerable<User> rawResult = _pageService.GetAll();

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

        public IEnumerable<Role> GetRoles()
        {
            return _roleService.GetAll();
        }

        public bool IsLoginExist(string login)
        {
            return _pageService.GetByColumn(nameof(User.Login), login).Any();
        }

        public UserViewModel GeByLogin(string login)
        {
            var rawResult = _pageService.GetByColumn(nameof(User.Login), login).FirstOrDefault();
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
    }
}
