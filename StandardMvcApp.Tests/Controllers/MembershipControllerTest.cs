using System.Web.Mvc;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurityGuard;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;
using StandardMvcApp.Controllers;

namespace StandardMvcApp.Tests.Controllers
{
    /// <summary>
    /// Summary description for MembershipControllerTest
    /// </summary>
    [TestClass]
    public class MembershipControllerTest
    {
        //private IMembershipService _membershipService;
        //private IAuthenticationService _authenticationService;
        private readonly Mock<MembershipProvider> _membershipProvider = new Mock<MembershipProvider>();
        private readonly Mock<IMembershipService> _membershipService = new Mock<IMembershipService>();
        private readonly Mock<IAuthenticationService> _authenticationService = new Mock<IAuthenticationService>();

        public MembershipControllerTest()
        {
            
            //_authenticationService = new AuthenticationService(_membershipService.Object);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void logon_test()
        {
            // Arrange
            //_authenticationService.Setup(s => s.LogOn(It.IsAny<LogOnViewModel>())).Returns(true);
            ////var controller = new MembershipController(_membershipService.Object, _authenticationService.Object);
            ////controller.Url.IsLocalUrl("~/");
            //var mockController = new Mock<MembershipController>();
            //mockController.se
            
            //LogOnViewModel model = new LogOnViewModel();
            //model.UserName = "joeuser";
            //model.Password = "joepassword";
            //model.RememberMe = true;

            //string returnUrl = "/";

            //// Act
            //var result = controller.LogOn(model, returnUrl) as RedirectToRouteResult;

            //// Assert
            //Assert.AreEqual(result.RouteValues["action"], "Index");
        }
    }
}
