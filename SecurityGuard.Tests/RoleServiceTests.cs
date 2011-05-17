using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecurityGuard.Interfaces;

namespace SecurityGuard.Tests
{
    [TestClass]
    public class RoleServiceTests
    {
        private Mock<IRoleService> mockRole;

        public RoleServiceTests()
        {
            mockRole = new Mock<IRoleService>();
        }

        [TestMethod]
        public void get_all_roles_list()
        {
            // Arrange
            mockRole.Setup(r => r.GetAllRoles()).Returns(new string[]
            {
                "Administrators",
                "Managers",
                "Sales",
                "Users"
            });

            // Act
            var list = mockRole.Object.GetAllRoles();

            // Assert
            Assert.AreEqual(4, list.Length);
        }

        #region Create methods
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void create_new_role_throwsexception_with_null_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.CreateRole(null)).Throws<ArgumentNullException>();

            // Act
            mockRole.Object.CreateRole(null);

            // Assert
            mockRole.Verify(r => r.CreateRole(null), Times.AtLeastOnce());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void create_new_role_throws_argumentexception_with_empty_string()
        {
            // Arrange
            mockRole.Setup(r => r.CreateRole("")).Throws<ArgumentException>();

            // Act
            mockRole.Object.CreateRole("");

            // Assert
            // Does not get here.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void create_new_role_throw_argumentexception_with_comma_in_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.CreateRole("role1,role2")).Throws<ArgumentException>();

            // Act
            mockRole.Object.CreateRole("role1,role2");

            // Assert
            // Does not get here.
        }

        [TestMethod]
        public void create_new_role_succeeds_with_valid_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.CreateRole("goodRoleName")).Verifiable();

            // Act
            mockRole.Object.CreateRole("goodRoleName");

            // Assert
            mockRole.Verify();
        } 
        #endregion

        #region Delete methods

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void delete_rolename_throws_exception_with_empty_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.DeleteRole(null)).Throws<ArgumentNullException>();

            // Act
            mockRole.Object.DeleteRole(null);

            // Assert
            mockRole.Verify(r => r.DeleteRole(null), Times.AtLeastOnce());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void delete_rolename_throws_exception_with_empty_string_as_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.DeleteRole("")).Throws<ArgumentException>();

            // Act
            mockRole.Object.DeleteRole("");

            // Assert
            //mockRole.Verify(r => r.DeleteRole(null), Times.AtLeastOnce());
        }

        [TestMethod]
        public void delete_rolename_succeeds_with_good_rolename()
        {
            // Arrange
            mockRole.Setup(r => r.DeleteRole("goodRoleName")).Returns(true);

            // Act
            bool actual = mockRole.Object.DeleteRole("goodRoleName");

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void delete_rolename_fails_when_role_has_one_or_more_members()
        {
            // Arrange
            mockRole.Setup(r => r.DeleteRole("goodRoleName", true)).Returns(false);

            // Act
            bool actual = mockRole.Object.DeleteRole("goodRoleName", true);

            // Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void delete_rolename_continues_when_role_has_one_or_more_members()
        {
            // Arrange
            mockRole.Setup(r => r.DeleteRole("goodRoleName", false)).Returns(true);

            // Act
            bool actual = mockRole.Object.DeleteRole("goodRoleName", false);

            // Assert
            Assert.AreEqual(true, actual);
        }

        #endregion
    }
}
