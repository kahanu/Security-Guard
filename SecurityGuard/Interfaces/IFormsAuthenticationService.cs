using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityGuard.Interfaces
{
    public interface IFormsAuthenticationService
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
