using EMR.Controllers;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace EMR.Web.Tests
{
    public class TestHomeController
    {

        private Mock<IUserPageService> _mockUserService;
        private Mock<IHomePageService> _mockHomeService;
        private List<UserViewModel> _users;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserPageService>();
            _mockHomeService = new Mock<IHomePageService>();

            _users = new List<UserViewModel>
            {
            new UserViewModel{Id = 1, RoleId = 1},
            new UserViewModel{Id = 2, RoleId = 2},
            new UserViewModel{Id = 3, RoleId = 3},
            new UserViewModel{Id = 4, RoleId = 4}
            };
        }

        [Test]
        public void CallHomeIndexWithLoggedDoctor_ReturnsRedirectToDoctorIndex()
        {
            var username = "FakeUserName";
            var identity = new GenericIdentity(username, "");

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole("Doctor")).Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(mockPrincipal.Object);

            var controller = new HomeController(_mockHomeService.Object, _mockUserService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                }
            };

            var testResult = new RedirectToActionResult("Index", "Doctors", null);

            var result = controller.Index() as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ActionName, testResult.ActionName);
            Assert.AreEqual(result.ControllerName, testResult.ControllerName);
        }

        [Test]
        public void CallHomeDetailsWithPatientUserId_ReturnsRedirectToPatientDetails()
        {
            int id = 1;
            _mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(_mockHomeService.Object, _mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var testResult = new RedirectToActionResult("Details", "Patients", new { userId = id });

            Assert.AreEqual(result.ActionName, testResult.ActionName);
            Assert.AreEqual(result.ControllerName, testResult.ControllerName);
            Assert.AreEqual(result.RouteValues, testResult.RouteValues);
        }

        [Test]
        public void CallHomeDetailsWithDoctorUserId_ReturnsRedirectToDoctorDetails()
        {
            int id = 2;
            _mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new Controllers.HomeController(_mockHomeService.Object, _mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var testResult = new RedirectToActionResult("Details", "Doctors", new { userId = id });

            Assert.AreEqual(result.ActionName, testResult.ActionName);
            Assert.AreEqual(result.ControllerName, testResult.ControllerName);
            Assert.AreEqual(result.RouteValues, testResult.RouteValues);
        }

        [Test]
        public void CallHomeDetailsWithEditorUserId_ReturnsRedirectToUserDetails()
        {
            int id = 3;
            _mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(_mockHomeService.Object, _mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var testResult = new RedirectToActionResult("Details", "Users", new { id = id });

            Assert.AreEqual(result.ActionName, testResult.ActionName);
            Assert.AreEqual(result.ControllerName, testResult.ControllerName);
            Assert.AreEqual(result.RouteValues, testResult.RouteValues);
        }

        [Test]
        public void CallHomeDetailsWithAdminUserId_ReturnsRedirectToUserDetails()
        {
            int id = 4;
            _mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(_mockHomeService.Object, _mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var testResult = new RedirectToActionResult("Details", "Users", new { id = id });

            Assert.AreEqual(result.ActionName, testResult.ActionName);
            Assert.AreEqual(result.ControllerName, testResult.ControllerName);
            Assert.AreEqual(result.RouteValues, testResult.RouteValues);
        }

        private UserViewModel GetTestUser(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }
}
