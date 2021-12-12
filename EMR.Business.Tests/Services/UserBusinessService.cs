using EMR.Business.Models;
using EMR.Business.Repositories;
using EMR.Business.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace EMR.Business.Tests
{
    public class TestUserBusinessService
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<ILogger<UserService>> _mockUserLogger;
        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockUserLogger = new Mock<ILogger<UserService>>();
            _users = new List<User>
            {
                new User{ Id = 1, RoleId = 1},
                new User{ Id = 2, RoleId = 1},
                new User{ Id = 3, RoleId = 1},
                new User{ Id = 4, RoleId = 2},
                new User{ Id = 5, RoleId = 2},
                new User{ Id = 6, RoleId = 3},
                new User{ Id = 7, RoleId = 3},
                new User{ Id = 8, RoleId = 4},
                new User{ Id = 9, RoleId = 4}
            };
        }

        [Test]
        public void GetUser_ById_ReturnsUserWithSameId()
        {
            int id = 2;
            _mockUserRepository.Setup(s => s.GetById(id)).Returns(GetTestUserById(id));
            var service = new UserService(_mockUserRepository.Object, _mockUserLogger.Object);
            var testUser = new User { Id = 2, RoleId = 1 };

            var result = service.GetById(id);

            Assert.AreEqual(JsonSerializer.Serialize(result), JsonSerializer.Serialize(testUser));
        }

        [Test]
        public void GetRandomUser_ByRoleId_ReturnsUserWithSameRoleId()
        {
            int roleId = 1;
            _mockUserRepository.Setup(s => s.GetByColumn("RoleId", roleId)).Returns(GetTestUserByRoleId(roleId));
            var service = new UserService(_mockUserRepository.Object, _mockUserLogger.Object);

            var result = service.GetRandomAccount(roleId);

            Assert.AreEqual(result.RoleId, roleId);
        }

        private User GetTestUserById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        private List<User> GetTestUserByRoleId(int roleId)
        {
            return _users.Where(x => x.RoleId == roleId).ToList();
        }
    }
}