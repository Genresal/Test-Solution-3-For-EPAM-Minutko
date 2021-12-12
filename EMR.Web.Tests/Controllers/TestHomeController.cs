using EMR.Controllers;
using EMR.Services;
using EMR.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Web.Tests
{
    class TestHomeController
    {
        [Test]
        public void HomeControllerIndexViewWhenUserIsPatient()
        {
            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();
            var controller = new HomeController(mockHomeService.Object, mockUserService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
{
    new Claim(ClaimTypes.Name, "example name"),
    new Claim(ClaimTypes.NameIdentifier, "1"),
    new Claim("custom-claim", "example claim value"),
}, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };



            /*
            var userMock = new Mock<IPrincipal>();
            userMock.Expect(p => p.IsInRole("User")).Returns(true);

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(ctx => ctx.User).Returns(new ClaimsPrincipal(new ClaimsIdentity(null, "Basic")));

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock.ExpectGet(con => con.HttpContext)
                                 .Returns(contextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;
            var result = controller.Index();
            userMock.Verify(p => p.IsInRole("User"));
            Assert.AreEqual(((ViewResult)result).ViewName, "Index");*/
        }
        [Test]
        public void Given_User_Index_Should_Return_ViewResult_With_Model()
        {
            //Arrange 
            var username = "FakeUserName";
            var identity = new GenericIdentity(username, "");

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole("Doctor")).Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(mockPrincipal.Object);

            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();

            var controller = new HomeController(mockHomeService.Object, mockUserService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                }
            };

            var ee = new RedirectToActionResult("Index", "Doctors", null);

            //Act
            var result = controller.Index() as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ActionName, ee.ActionName);
            Assert.AreEqual(result.ControllerName, ee.ControllerName);
        }

            [Test]
        public void HomeControllerRedirectToPatientDetails()
        {
            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();
            int id = 1;
            mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(mockHomeService.Object, mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var ee = new RedirectToActionResult("Details", "Patients", new {userId = id });

            Assert.AreEqual(result.ActionName, ee.ActionName);
            Assert.AreEqual(result.ControllerName, ee.ControllerName);
            Assert.AreEqual(result.RouteValues, ee.RouteValues);
        }
        [Test]
        public void HomeControllerRedirectToDoctorDetails()
        {
            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();
            int id = 2;
            mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new Controllers.HomeController(mockHomeService.Object, mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var ee = new RedirectToActionResult("Details", "Doctors", new { userId = id });

            Assert.AreEqual(result.ActionName, ee.ActionName);
            Assert.AreEqual(result.ControllerName, ee.ControllerName);
            Assert.AreEqual(result.RouteValues, ee.RouteValues);
        }
        [Test]
        public void HomeControllerRedirectEditorToUserDetails()
        {
            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();
            int id = 3;
            mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(mockHomeService.Object, mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var ee = new RedirectToActionResult("Details", "Users", new { id = id });

            Assert.AreEqual(result.ActionName, ee.ActionName);
            Assert.AreEqual(result.ControllerName, ee.ControllerName);
            Assert.AreEqual(result.RouteValues, ee.RouteValues);
        }
        [Test]
        public void HomeControllerRedirectAdminToUserDetails()
        {
            var mockUserService = new Mock<IUserPageService>();
            var mockHomeService = new Mock<IHomePageService>();
            int id = 4;
            mockUserService.Setup(s => s.GetById(id)).Returns(GetTestUser(id));
            var controller = new HomeController(mockHomeService.Object, mockUserService.Object);

            var result = controller.Details(id) as RedirectToActionResult;

            var ee = new RedirectToActionResult("Details", "Users", new { id = id });

            Assert.AreEqual(result.ActionName, ee.ActionName);
            Assert.AreEqual(result.ControllerName, ee.ControllerName);
            Assert.AreEqual(result.RouteValues, ee.RouteValues);
        }

        private UserViewModel GetTestUser(int id)
        {
            var users = new List<UserViewModel>
            {
            new UserViewModel{ Id = 1, RoleId = 1 },
            new UserViewModel{Id = 2, RoleId = 2},
            new UserViewModel{Id = 3, RoleId = 3},
            new UserViewModel{Id = 4, RoleId = 4}
            };

            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
