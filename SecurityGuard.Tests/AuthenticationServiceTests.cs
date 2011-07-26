using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurityGuard.Services;
using SecurityGuard.Interfaces;
using SecurityGuard.ViewModels;

namespace SecurityGuard.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AuthenticationServiceTests
    {
        private readonly Mock<IMembershipService> _membershipService;
        private readonly Mock<AuthenticationService> _authenticationService;
        private readonly Mock<IFormsAuthenticationService> _formsAuthenticationService;

        public AuthenticationServiceTests()
        {
            _membershipService = new Mock<IMembershipService>();
            _formsAuthenticationService = new Mock<IFormsAuthenticationService>();
            _authenticationService = new Mock<AuthenticationService>(_membershipService.Object, _formsAuthenticationService.Object);
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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
        public void log_on_test()
        {
            // Arrange
            _membershipService.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            LogOnViewModel model = new LogOnViewModel();
            model.UserName = "joeuser";
            model.Password = "joepassword";
            model.RememberMe = true;

            // Act
            var result = _authenticationService.Object.LogOn(model.UserName, model.Password, model.RememberMe);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
