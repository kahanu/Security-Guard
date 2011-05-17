using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurityGuard.Interfaces;

namespace SecurityGuard.Tests
{
    [TestClass]
    public class MembershipServiceTests
    {
        private readonly Mock<IMembershipService> _membershipService;

        public MembershipServiceTests()
        {
            _membershipService = new Mock<IMembershipService>();
        }


        #region Property Tests
        [TestMethod]
        public void get_application_name()
        {
            // Arrange
            _membershipService.Setup(m => m.ApplicationName).Returns("MyApplication");

            // Act
            var result = _membershipService.Object.ApplicationName;

            // Assert
            Assert.AreEqual("MyApplication", result);
        }

        [TestMethod]
        public void requires_question_and_answer_is_true()
        {
            // Arrange
            _membershipService.Setup(m => m.RequiresQuestionAndAnswer).Returns(true);

            // Act
            var actual = _membershipService.Object.RequiresQuestionAndAnswer;

            // Assert
            Assert.AreEqual(true, actual);
        } 
        #endregion

        #region Get Tests



        #endregion

        #region Miscellaneous Tests

        [TestMethod]
        public void split_string_to_array()
        {
            // Arrange
            string stringToSplit = "Sales,Manager,";

            // Act
            string[] split = stringToSplit.Substring(0, stringToSplit.Length - 1).Split(',');

            // Assert
            Assert.AreEqual(2, split.Length);
            Assert.AreEqual("Manager", split[1]);
        }

        #endregion
    }
}
