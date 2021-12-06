using EMR.Business.Models;
using EMR.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMR.Business.Services
{
    public class UserService : BaseBusinessService<User>, IUserService
    {
        public UserService(IRepository<User> r) : base (r)
        {
        }

        public User GetRandomAccount(int roleId)
        {
            var accounts = _mainRepository.GetByColumn(nameof(User.RoleId), roleId).ToList();
            var random = new Random();
            return accounts[random.Next(accounts.Count)];
        }
    }
}
