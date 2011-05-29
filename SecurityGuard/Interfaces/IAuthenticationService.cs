namespace SecurityGuard.Interfaces
{
    public interface IAuthenticationService
    {
        bool LogOn(string userName, string password, bool rememberMe);
        void LogOff();
    }
}
