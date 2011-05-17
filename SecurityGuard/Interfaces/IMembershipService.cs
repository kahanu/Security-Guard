using System.Web.Security;

namespace SecurityGuard.Interfaces
{
    public interface IMembershipService
    {
        // Summary:
        //     Gets or sets the name of the application.
        //
        // Returns:
        //     The name of the application.
        string ApplicationName { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the current membership provider is configured
        //     to allow users to reset their passwords.
        //
        // Returns:
        //     true if the membership provider supports password reset; otherwise, false.
        bool EnablePasswordReset { get; }
        //
        // Summary:
        //     Gets a value indicating whether the current membership provider is configured
        //     to allow users to retrieve their passwords.
        //
        // Returns:
        //     true if the membership provider supports password retrieval; otherwise, false.
        bool EnablePasswordRetrieval { get; }
        //
        // Summary:
        //     The identifier of the algorithm used to hash passwords.
        //
        // Returns:
        //     The identifier of the algorithm used to hash passwords, or blank to use the
        //     default hash algorithm.
        string HashAlgorithmType { get; }
        //
        // Summary:
        //     Gets the number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        //
        // Returns:
        //     The number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        int MaxInvalidPasswordAttempts { get; }
        //
        // Summary:
        //     Gets the minimum number of special characters that must be present in a valid
        //     password.
        //
        // Returns:
        //     The minimum number of special characters that must be present in a valid
        //     password.
        int MinRequiredNonAlphanumericCharacters { get; }
        //
        // Summary:
        //     Gets the minimum length required for a password.
        //
        // Returns:
        //     The minimum length required for a password.
        int MinRequiredPasswordLength { get; }
        //
        // Summary:
        //     Gets the time window between which consecutive failed attempts to provide
        //     a valid password or password answer are tracked.
        //
        // Returns:
        //     The time window, in minutes, during which consecutive failed attempts to
        //     provide a valid password or password answer are tracked. The default is 10
        //     minutes. If the interval between the current failed attempt and the last
        //     failed attempt is greater than the System.Web.Security.Membership.PasswordAttemptWindow
        //     property setting, each failed attempt is treated as if it were the first
        //     failed attempt.
        int PasswordAttemptWindow { get; }
        //
        // Summary:
        //     Gets the regular expression used to evaluate a password.
        //
        // Returns:
        //     A regular expression used to evaluate a password.
        string PasswordStrengthRegularExpression { get; }
        //
        // Summary:
        //     Gets a reference to the default membership provider for the application.
        //
        // Returns:
        //     The default membership provider for the application exposed using the System.Web.Security.MembershipProvider
        //     abstract base class.
        MembershipProvider Provider { get; }
        //
        // Summary:
        //     Gets a collection of the membership providers for the ASP.NET application.
        //
        // Returns:
        //     A System.Web.Security.MembershipProviderCollection of the membership providers
        //     configured for the ASP.NET application.
        MembershipProviderCollection Providers { get; }
        //
        // Summary:
        //     Gets a value indicating whether the default membership provider requires
        //     the user to answer a password question for password reset and retrieval.
        //
        // Returns:
        //     true if a password answer is required for password reset and retrieval; otherwise,
        //     false.
        bool RequiresQuestionAndAnswer { get; }
        //
        // Summary:
        //     Specifies the number of minutes after the last-activity date/time stamp for
        //     a user during which the user is considered online.
        //
        // Returns:
        //     The number of minutes after the last-activity date/time stamp for a user
        //     during which the user is considered online.
        int UserIsOnlineTimeWindow { get; }

        // Summary:
        //     Occurs when a user is created, a password is changed, or a password is reset.
        event MembershipValidatePasswordEventHandler ValidatingPassword;

        // Summary:
        //     Adds a new user to the data store.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object for the newly created user.
        //
        // Exceptions:
        //   System.Web.Security.MembershipCreateUserException:
        //     The user was not created. Check the System.Web.Security.MembershipCreateUserException.StatusCode
        //     property for a System.Web.Security.MembershipCreateStatus value.
        MembershipUser CreateUser(string username, string password);
        //
        // Summary:
        //     Adds a new user with a specified e-mail address to the data store.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        //   email:
        //     The e-mail address for the new user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object for the newly created user.
        //
        // Exceptions:
        //   System.Web.Security.MembershipCreateUserException:
        //     The user was not created. Check the System.Web.Security.MembershipCreateUserException.StatusCode
        //     property for a System.Web.Security.MembershipCreateStatus value.
        MembershipUser CreateUser(string username, string password, string email);
        //
        // Summary:
        //     Adds a new user with specified property values to the data store and returns
        //     a status parameter indicating that the user was successfully created or the
        //     reason the user creation failed.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        //   email:
        //     The e-mail address for the new user.
        //
        //   passwordQuestion:
        //     The password-question value for the membership user.
        //
        //   passwordAnswer:
        //     The password-answer value for the membership user.
        //
        //   isApproved:
        //     A Boolean that indicates whether the new user is approved to log on.
        //
        //   status:
        //     A System.Web.Security.MembershipCreateStatus indicating that the user was
        //     created successfully or the reason that creation failed.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object for the newly created user. If
        //     no user was created, this method returns null.
        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status);
        //
        // Summary:
        //     Adds a new user with specified property values and a unique identifier to
        //     the data store and returns a status parameter indicating that the user was
        //     successfully created or the reason the user creation failed.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        //   email:
        //     The e-mail address for the new user.
        //
        //   passwordQuestion:
        //     The password-question value for the membership user.
        //
        //   passwordAnswer:
        //     The password-answer value for the membership user.
        //
        //   isApproved:
        //     A Boolean that indicates whether the new user is approved to log on.
        //
        //   providerUserKey:
        //     The user identifier for the user that should be stored in the membership
        //     data store.
        //
        //   status:
        //     A System.Web.Security.MembershipCreateStatus indicating that the user was
        //     created successfully or the reason creation failed.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object for the newly created user. If
        //     no user was created, this method returns null.
        MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);
        //
        // Summary:
        //     Deletes a user and any related user data from the database.
        //
        // Parameters:
        //   username:
        //     The name of the user to delete.
        //
        // Returns:
        //     true if the user was deleted; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     username is an empty string or contains a comma (,).
        //
        //   System.ArgumentNullException:
        //     username is null.
        bool DeleteUser(string username);
        //
        // Summary:
        //     Deletes a user from the database.
        //
        // Parameters:
        //   username:
        //     The name of the user to delete.
        //
        //   deleteAllRelatedData:
        //     true to delete data related to the user from the database; false to leave
        //     data related to the user in the database.
        //
        // Returns:
        //     true if the user was deleted; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     username is an empty string or contains a comma (,).
        //
        //   System.ArgumentNullException:
        //     username is null.
        bool DeleteUser(string username, bool deleteAllRelatedData);
        //
        // Summary:
        //     Gets a collection of membership users where the e-mail address contains the
        //     specified e-mail address to match.
        //
        // Parameters:
        //   emailToMatch:
        //     The e-mail address to search for.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection that contains all users that
        //     match the emailToMatch parameter.  Leading and trailing spaces are trimmed
        //     from the emailToMatch parameter value.
        MembershipUserCollection FindUsersByEmail(string emailToMatch);
        //
        // Summary:
        //     Gets a collection of membership users, in a page of data, where the e-mail
        //     address contains the specified e-mail address to match.
        //
        // Parameters:
        //   emailToMatch:
        //     The e-mail address to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection that contains a page of pageSizeSystem.Web.Security.MembershipUser
        //     objects beginning at the page specified by pageIndex.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     pageIndex is less than zero.  -or- pageSize is less than 1.
        MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);
        //
        // Summary:
        //     Gets a collection of membership users where the user name contains the specified
        //     user name to match.
        //
        // Parameters:
        //   usernameToMatch:
        //     The user name to search for.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection that contains all users that
        //     match the usernameToMatch parameter.  Leading and trailing spaces are trimmed
        //     from the usernameToMatch parameter value.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     usernameToMatch is an empty string.
        //
        //   System.ArgumentNullException:
        //     usernameToMatch is null.
        MembershipUserCollection FindUsersByName(string usernameToMatch);
        //
        // Summary:
        //     Gets a collection of membership users, in a page of data, where the user
        //     name contains the specified user name to match.
        //
        // Parameters:
        //   usernameToMatch:
        //     The user name to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection that contains a page of pageSizeSystem.Web.Security.MembershipUser
        //     objects beginning at the page specified by pageIndex.  Leading and trailing
        //     spaces are trimmed from the usernameToMatch parameter value.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     usernameToMatch is an empty string.  -or- pageIndex is less than zero.  -or-
        //     pageSize is less than 1.
        //
        //   System.ArgumentNullException:
        //     usernameToMatch is null.
        MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);
        //
        // Summary:
        //     Generates a random password of the specified length.
        //
        // Parameters:
        //   length:
        //     The number of characters in the generated password. The length must be between
        //     1 and 128 characters.
        //
        //   numberOfNonAlphanumericCharacters:
        //     The minimum number of punctuation characters in the generated password.
        //
        // Returns:
        //     A random password of the specified length.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     length is less than 1 or greater than 128 -or- numberOfNonAlphanumericCharacters
        //     is less than 0 or greater than length.
        string GeneratePassword(int length, int numberOfNonAlphanumericCharacters);
        //
        // Summary:
        //     Gets a collection of all the users in the database.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection of System.Web.Security.MembershipUser
        //     objects representing all of the users in the database.
        MembershipUserCollection GetAllUsers();
        //
        // Summary:
        //     Gets a collection of all the users in the database in pages of data.
        //
        // Parameters:
        //   pageIndex:
        //     The index of the page of results to return. Use 0 to indicate the first page.
        //
        //   pageSize:
        //     The size of the page of results to return. pageIndex is zero-based.
        //
        //   totalRecords:
        //     The total number of users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection of System.Web.Security.MembershipUser
        //     objects representing all the users in the database for the configured applicationName.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     pageIndex is less than zero.  -or- pageSize is less than 1.
        MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
        //
        // Summary:
        //     Gets the number of users currently accessing an application.
        //
        // Returns:
        //     The number of users currently accessing an application.
        int GetNumberOfUsersOnline();
        //
        // Summary:
        //     Gets the information from the data source and updates the last-activity date/time
        //     stamp for the current logged-on membership user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the current logged-on
        //     user.
        MembershipUser GetUser();
        //
        // Summary:
        //     Gets the information from the data source for the current logged-on membership
        //     user. Updates the last-activity date/time stamp for the current logged-on
        //     membership user, if specified.
        //
        // Parameters:
        //   userIsOnline:
        //     If true, updates the last-activity date/time stamp for the specified user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the current logged-on
        //     user.
        MembershipUser GetUser(bool userIsOnline);
        //
        // Summary:
        //     Gets the information from the data source for the membership user associated
        //     with the specified unique identifier.
        //
        // Parameters:
        //   providerUserKey:
        //     The unique user identifier from the membership data source for the user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the user associated
        //     with the specified unique identifier.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     providerUserKey is null.
        MembershipUser GetUser(object providerUserKey);
        //
        // Summary:
        //     Gets the information from the data source for the specified membership user.
        //
        // Parameters:
        //   username:
        //     The name of the user to retrieve.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the specified user.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     username contains a comma (,).
        //
        //   System.ArgumentNullException:
        //     username is null.
        MembershipUser GetUser(string username);
        //
        // Summary:
        //     Gets the information from the data source for the membership user associated
        //     with the specified unique identifier. Updates the last-activity date/time
        //     stamp for the user, if specified.
        //
        // Parameters:
        //   providerUserKey:
        //     The unique user identifier from the membership data source for the user.
        //
        //   userIsOnline:
        //     If true, updates the last-activity date/time stamp for the specified user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the user associated
        //     with the specified unique identifier.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     providerUserKey is null.
        MembershipUser GetUser(object providerUserKey, bool userIsOnline);
        //
        // Summary:
        //     Gets the information from the data source for the specified membership user.
        //     Updates the last-activity date/time stamp for the user, if specified.
        //
        // Parameters:
        //   username:
        //     The name of the user to retrieve.
        //
        //   userIsOnline:
        //     If true, updates the last-activity date/time stamp for the specified user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object representing the specified user.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     username contains a comma (,).
        //
        //   System.ArgumentNullException:
        //     username is null.
        MembershipUser GetUser(string username, bool userIsOnline);
        //
        // Summary:
        //     Gets a user name where the e-mail address for the user matches the specified
        //     e-mail address.
        //
        // Parameters:
        //   emailToMatch:
        //     The e-mail address to search for.
        //
        // Returns:
        //     The user name where the e-mail address for the user matches the specified
        //     e-mail address. If no match is found, null is returned.
        string GetUserNameByEmail(string emailToMatch);
        //
        // Summary:
        //     Updates the database with the information for the specified user.
        //
        // Parameters:
        //   user:
        //     A System.Web.Security.MembershipUser object that represents the user to be
        //     updated and the updated information for the user.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     user is null.
        void UpdateUser(MembershipUser user);
        //
        // Summary:
        //     Verifies that the supplied user name and password are valid.
        //
        // Parameters:
        //   username:
        //     The name of the user to be validated.
        //
        //   password:
        //     The password for the specified user.
        //
        // Returns:
        //     true if the supplied user name and password are valid; otherwise, false.
        bool ValidateUser(string username, string password);

    }
}
