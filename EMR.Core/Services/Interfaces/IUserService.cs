using EMR.Business.Models;

namespace EMR.Business.Services
{
    public interface IUserService : IBusinessService<User>
    {
        public User GetRandomAccount(int roleId);
        public void ChangePassword(int id, string password);
    }
}
