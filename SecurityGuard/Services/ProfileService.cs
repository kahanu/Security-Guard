using System.Web.Profile;
using SecurityGuard.Interfaces;

namespace SecurityGuard.Services
{
    public class ProfileService : IProfileService
    {

        #region ctors

        private readonly ProfileProvider profileProvider;

        public ProfileService(ProfileProvider profileProvider)
        {
            this.profileProvider = profileProvider;
        }

        #endregion

        #region IProfileService Members

        public int DeleteInactiveProfiles(System.Web.Profile.ProfileAuthenticationOption authenticationOption, System.DateTime userInactiveSinceDate)
        {
            return profileProvider.DeleteInactiveProfiles(authenticationOption, userInactiveSinceDate);
        }

        public int DeleteProfiles(string[] usernames)
        {
            return profileProvider.DeleteProfiles(usernames);
        }

        public int DeleteProfiles(System.Web.Profile.ProfileInfoCollection profiles)
        {
            return profileProvider.DeleteProfiles(profiles);
        }

        public System.Web.Profile.ProfileInfoCollection FindInactiveProfilesByUserName(System.Web.Profile.ProfileAuthenticationOption authenticationOption, string usernameToMatch, System.DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            return profileProvider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
        }

        public System.Web.Profile.ProfileInfoCollection FindProfilesByUserName(System.Web.Profile.ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return profileProvider.FindProfilesByUserName(authenticationOption, usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public System.Web.Profile.ProfileInfoCollection GetAllInactiveProfiles(System.Web.Profile.ProfileAuthenticationOption authenticationOption, System.DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            return profileProvider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
        }

        public System.Web.Profile.ProfileInfoCollection GetAllProfiles(System.Web.Profile.ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            return profileProvider.GetAllProfiles(authenticationOption, pageIndex, pageSize, out totalRecords);
        }

        public int GetNumberOfInactiveProfiles(System.Web.Profile.ProfileAuthenticationOption authenticationOption, System.DateTime userInactiveSinceDate)
        {
            return profileProvider.GetNumberOfInactiveProfiles(authenticationOption, userInactiveSinceDate);
        }

        public string ApplicationName
        {
            get
            {
                return profileProvider.ApplicationName;
            }
            set
            {
                profileProvider.ApplicationName = value;
            }
        }

        public System.Configuration.SettingsPropertyValueCollection GetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyCollection collection)
        {
            return profileProvider.GetPropertyValues(context, collection);
        }

        public void SetPropertyValues(System.Configuration.SettingsContext context, System.Configuration.SettingsPropertyValueCollection collection)
        {
            profileProvider.SetPropertyValues(context, collection);
        }

        #endregion
    }
}
