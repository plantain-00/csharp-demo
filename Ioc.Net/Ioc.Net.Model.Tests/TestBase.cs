using Ioc.Net.Model.Businesses;
using Ioc.Net.Model.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ioc.Net.Model.Tests
{
    public class TestBase
    {
        [TestInitialize]
        public void Init()
        {
            Ioc.Map<IUserService, UserService>();
            Ioc.Map<IUserBusiness, UserBusiness>();
        }
    }
}
