using SecurityGuard.ViewModels;

namespace SecurityGuard.Interfaces
{
    public interface IAuthenticationService
    {
        bool LogOn(LogOnViewModel model);
        void LogOff();
    }
}
