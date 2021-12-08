using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;

namespace EMR.Business.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    public interface IUserService : IBusinessService<User>
    {
        public User GetRandomAccount(int roleId);
        public void ChangePassword(int id, string password);
    }
}
