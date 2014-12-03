using FluentAssertions;

using Ioc.Net.Model.Controllers;
using Ioc.Net.Model.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Ioc.Net.Model.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests : TestBase
    {
        private readonly Mock<HomeController> _homeController = new Mock<HomeController>();
        
        [TestMethod]
        public void HomeController_Index_Test()
        {
            _homeController.Setup(c => c.UserBusiness.GetUsers()).Returns(new[]
                                                                             {
                                                                                 new User
                                                                                 {
                                                                                     ID = 1,
                                                                                     Name = "aa"
                                                                                 }
                                                                             });
            _homeController.Setup(c => c.UserBusiness.UserService.CurrentUser).Returns(new User
                                                                                       {
                                                                                           ID = 3,
                                                                                           Name = "cc"
                                                                                       });
            _homeController.Setup(c => c.UserBusiness.UserService.GetSession<string>("name")).Returns("name");
            _homeController.Object.Index();
            (_homeController.Object.ViewData["users"] as User[]).Should().Contain(i => i.ID == 1);
        }

        [TestMethod]
        public void HomeController_AddUsers_Test()
        {
            _homeController.Setup(c => c.UserBusiness.AddUsers("a", "b", "c"));
            _homeController.Object.AddUsers("a", "b", "c");
        }
    }
}
