using EMR.Business.Models;
using EMR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Mapper
{
    public static class Mapper
    {
        public static RecordViewModel ToViewModel(this Record record)
        {
            if (record != null)
            {
                return new RecordViewModel
                {
                    Id = record.Id,
                    Diagnosis = $"({record.DiagnosisId.ToString("000")}) {record.Diagnosis.Name}",
                    PatientName = $"{record.Patient.User.FirstName} {record.Patient.User.LastName}",
                    Doctor = $"Dr. {record.Doctor.User.FirstName} {record.Doctor.User.LastName}",
                    DoctorPosition = record.Doctor.Position.Name,
                    DoctorPositionId = record.Doctor.PositionId,
                    ModifiedDate = record.ModifiedDate
                };
            }
            return null;
        }

        public static Patient ToModel(this PatientViewModel model)
        {
            if (model != null)
            {
                return new Patient
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Job = model.Job,
                    User = new User
                    {
                        Id = model.UserId,
                        Login = model.Login,
                        Password = model.Password,
                        RoleId = model.RoleId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Birthday = model.Birthday,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        PhotoUrl = model.PhotoUrl
                    }
                };
            }
            return null;
        }

        public static PatientViewModel ToViewModel(this Patient model)
        {
            if (model != null)
            {
                return new PatientViewModel
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Job = model.Job,
                    RoleId = model.User.RoleId,
                    Login = model.User.Login,
                    Password = model.User.Password,
                    FirstName = model.User.FirstName,
                    LastName = model.User.LastName,
                    Birthday = model.User.Birthday,
                    PhoneNumber = model.User.PhoneNumber,
                    Email = model.User.Email,
                    PhotoUrl = model.User.PhotoUrl
                    
                };
            }
            return null;
        }

        public static ServiceUserViewModel ToViewModel(this User model)
        {
            if (model != null)
            {
                return new ServiceUserViewModel
                {
                    Id = model.Id,
                    RoleId = model.RoleId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    PhotoUrl = model.PhotoUrl

                };
            }
            return null;
        }

        public static User ToModel(this ServiceUserViewModel model)
        {
            if (model != null)
            {
                return new User
                {
                    Id = model.Id,
                    RoleId = model.RoleId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    PhotoUrl = model.PhotoUrl

                };
            }
            return null;
        }

        public static ServiceUserViewModel ToServiceUser(this RegisterViewModel model)
        {
            if (model != null)
            {
                return new ServiceUserViewModel
                {
                    RoleId = model.RoleId,
                    Login = model.Login,
                    Password = model.Password
                };
            }
            return null;
        }

        public static PatientViewModel ToPatient(this RegisterViewModel model)
        {
            if (model != null)
            {
                return new PatientViewModel
                {
                    RoleId = model.RoleId,
                    Login = model.Login,
                    Password = model.Password
                };
            }
            return null;
        }

        public static DoctorViewModel ToDoctor(this RegisterViewModel model)
        {
            if (model != null)
            {
                return new DoctorViewModel
                {
                    RoleId = model.RoleId,
                    Login = model.Login,
                    Password = model.Password
                };
            }
            return null;
        }
    }
}
