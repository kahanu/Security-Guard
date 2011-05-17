using System.Web.Security;
using SecurityGuard.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace SecurityGuard.Services
{
    public class RoleService : IRoleService
    {

        #region ctors

        private readonly RoleProvider roleProvider;

        public RoleService(RoleProvider roleProvider)
        {
            this.roleProvider = roleProvider;
        }

        #endregion

        #region IRoleService Members

        public string ApplicationName
        {
            get
            {
                return roleProvider.ApplicationName;
            }
            set
            {
                roleProvider.ApplicationName = value;
            }
        }

        public bool CacheRolesInCookie
        {
            get { throw new System.NotImplementedException(); }
        }

        public string CookieName
        {
            get { throw new System.NotImplementedException(); }
        }

        public string CookiePath
        {
            get { throw new System.NotImplementedException(); }
        }

        public System.Web.Security.CookieProtection CookieProtectionValue
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool CookieRequireSSL
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool CookieSlidingExpiration
        {
            get { throw new System.NotImplementedException(); }
        }

        public int CookieTimeout
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool CreatePersistentCookie
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Domain
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Enabled
        {
            get { throw new System.NotImplementedException(); }
        }

        public int MaxCachedResults
        {
            get { throw new System.NotImplementedException(); }
        }

        public System.Web.Security.RoleProvider Provider
        {
            get { return roleProvider; }
        }

        public System.Web.Security.RoleProviderCollection Providers
        {
            get { throw new System.NotImplementedException(); }
        }

        public void AddUsersToRole(string[] usernames, string roleName)
        {
            roleProvider.AddUsersToRoles(usernames, new string[] { roleName });
        }

        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            roleProvider.AddUsersToRoles(usernames, roleNames);
        }

        public void AddUserToRole(string username, string roleName)
        {
            roleProvider.AddUsersToRoles(new string[] { username }, new string[] { roleName });
        }

        public void AddUserToRoles(string username, string[] roleNames)
        {
            roleProvider.AddUsersToRoles(new string[] { username }, roleNames);
        }

        public void CreateRole(string roleName)
        {
            roleProvider.CreateRole(roleName);
        }

        public void DeleteCookie()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRole(string roleName)
        {
            return roleProvider.DeleteRole(roleName, true);
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return roleProvider.DeleteRole(roleName, throwOnPopulatedRole);
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return roleProvider.FindUsersInRole(roleName, usernameToMatch);
        }

        public string[] GetAllRoles()
        {
            return roleProvider.GetAllRoles();
        }

        public string[] GetRolesForUser()
        {
            return roleProvider.GetRolesForUser(null);
        }

        public string[] GetRolesForUser(string username)
        {
            return roleProvider.GetRolesForUser(username);
        }

        public string[] GetUsersInRole(string roleName)
        {
            return roleProvider.GetUsersInRole(roleName);
        }

        public bool IsUserInRole(string roleName)
        {
            return roleProvider.IsUserInRole(null, roleName);
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return roleProvider.IsUserInRole(username, roleName);
        }

        public void RemoveUserFromRole(string username, string roleName)
        {
            roleProvider.RemoveUsersFromRoles(new string[] { username }, new string[] { roleName });
        }

        public void RemoveUserFromRoles(string username, string[] roleNames)
        {
            roleProvider.RemoveUsersFromRoles(new string[] { username }, roleNames);
        }

        public void RemoveUsersFromRole(string[] usernames, string roleName)
        {
            roleProvider.RemoveUsersFromRoles(usernames, new string[] { roleName });
        }

        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            roleProvider.RemoveUsersFromRoles(usernames, roleNames);
        }

        public bool RoleExists(string roleName)
        {
            return roleProvider.RoleExists(roleName);
        }

        #endregion

        #region Additional Members

        /// <summary>
        /// Returns a list of Roles for which the User is not granted.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<string> AvailableRolesForUser(string userName)
        {
            string[] allRoles = roleProvider.GetAllRoles();
            string[] rolesForUser = roleProvider.GetRolesForUser(userName);
            IEnumerable<string> availabileRolesForUser;

            availabileRolesForUser = allRoles.Except(rolesForUser);

            return availabileRolesForUser;
        }

        #endregion
    }
}
