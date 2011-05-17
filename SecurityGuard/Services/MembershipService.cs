using System.Web.Security;
using SecurityGuard.Interfaces;

namespace SecurityGuard.Services
{
    public class MembershipService : IMembershipService
    {

        #region ctors
        private readonly MembershipProvider membershipProvider;

        public MembershipService(MembershipProvider membershipProvider)
        {
            this.membershipProvider = membershipProvider;
        }
        #endregion

        #region IMembershipService Members

        public string ApplicationName
        {
            get
            {
                return membershipProvider.ApplicationName;
            }
            set
            {
                membershipProvider.ApplicationName = value;
            }
        }

        public bool EnablePasswordReset
        {
            get { return membershipProvider.EnablePasswordReset; }
        }

        public bool EnablePasswordRetrieval
        {
            get { return membershipProvider.EnablePasswordRetrieval; }
        }

        public string HashAlgorithmType
        {
            get { return membershipProvider.PasswordFormat.ToString(); }
        }

        public int MaxInvalidPasswordAttempts
        {
            get { return membershipProvider.MaxInvalidPasswordAttempts; }
        }

        public int MinRequiredNonAlphanumericCharacters
        {
            get { return membershipProvider.MinRequiredNonAlphanumericCharacters; }
        }

        public int MinRequiredPasswordLength
        {
            get { return membershipProvider.MinRequiredPasswordLength; }
        }

        public int PasswordAttemptWindow
        {
            get { return membershipProvider.PasswordAttemptWindow; }
        }

        public string PasswordStrengthRegularExpression
        {
            get { return membershipProvider.PasswordStrengthRegularExpression; }
        }

        public System.Web.Security.MembershipProvider Provider
        {
            get { return membershipProvider; }
        }

        public System.Web.Security.MembershipProviderCollection Providers
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool RequiresQuestionAndAnswer
        {
            get { return membershipProvider.RequiresQuestionAndAnswer; }
        }

        public int UserIsOnlineTimeWindow
        {
            get { throw new System.NotImplementedException(); }
        }

        public event System.Web.Security.MembershipValidatePasswordEventHandler ValidatingPassword;

        public System.Web.Security.MembershipUser CreateUser(string username, string password)
        {
            MembershipCreateStatus status;
            return membershipProvider.CreateUser(username, password, null, null, null, true, null, out status);
        }

        public System.Web.Security.MembershipUser CreateUser(string username, string password, string email)
        {
            MembershipCreateStatus status;
            return membershipProvider.CreateUser(username, password, email, null, null, true, null, out status);
        }

        public System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out System.Web.Security.MembershipCreateStatus status)
        {
            return membershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, null, out status);
        }

        public System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            return membershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public bool DeleteUser(string username)
        {
            return membershipProvider.DeleteUser(username, true);
        }

        public bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return membershipProvider.DeleteUser(username, deleteAllRelatedData);
        }

        public System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch)
        {
            int totalRecords;
            return membershipProvider.FindUsersByEmail(emailToMatch, 0, 10, out totalRecords);
        }

        public System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return membershipProvider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch)
        {
            int totalRecords;
            return membershipProvider.FindUsersByName(usernameToMatch, 0, 10, out totalRecords);
        }

        public System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return membershipProvider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            throw new System.NotImplementedException();
        }

        public System.Web.Security.MembershipUserCollection GetAllUsers()
        {
            int totalRecords;
            return membershipProvider.GetAllUsers(0, 20, out totalRecords);
        }

        public System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return membershipProvider.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public int GetNumberOfUsersOnline()
        {
            return membershipProvider.GetNumberOfUsersOnline();
        }

        public System.Web.Security.MembershipUser GetUser()
        {
            return membershipProvider.GetUser(null, false);
        }

        public System.Web.Security.MembershipUser GetUser(bool userIsOnline)
        {
            return membershipProvider.GetUser(null, userIsOnline);
        }

        public System.Web.Security.MembershipUser GetUser(object providerUserKey)
        {
            return membershipProvider.GetUser(providerUserKey, false);
        }

        public System.Web.Security.MembershipUser GetUser(string username)
        {
            return membershipProvider.GetUser(username, false);
        }

        public System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return membershipProvider.GetUser(providerUserKey, userIsOnline);
        }

        public System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            return membershipProvider.GetUser(username, userIsOnline);
        }

        public string GetUserNameByEmail(string emailToMatch)
        {
            return membershipProvider.GetUserNameByEmail(emailToMatch);
        }

        public void UpdateUser(System.Web.Security.MembershipUser user)
        {
            membershipProvider.UpdateUser(user);
        }

        public bool ValidateUser(string username, string password)
        {
            return membershipProvider.ValidateUser(username, password);
        }

        #endregion
    }
}
