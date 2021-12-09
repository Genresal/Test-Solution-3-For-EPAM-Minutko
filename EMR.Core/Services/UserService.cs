using EMR.Business.Models;
using EMR.Business.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EMR.Business.Services
{
    public class UserService : BaseBusinessService<User>, IUserService
    {
        public UserService(IRepository<User> repository, ILogger<UserService> logger) : base(repository, logger)
        {
        }

        public User GetRandomAccount(int roleId)
        {
            var accounts = _mainRepository.GetByColumn(nameof(User.RoleId), roleId).ToList();
            var random = new Random();
            return accounts[random.Next(accounts.Count)];
        }

        public void ChangePassword(int id, string password)
        {
            _mainRepository.Update(nameof(User.Password), password, id);
        }
    }
}
