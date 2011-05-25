using System.Configuration;

namespace SecurityGuard.Core
{
    public static class ConfigSettings
    {
        public static string SecurityGuardEmailFrom { get { return ConfigurationManager.AppSettings["SecurityGuardEmailFrom"]; } }
        public static string SecurityGuardEmailSubject { get { return ConfigurationManager.AppSettings["SecurityGuardEmailSubject"]; } }
        public static string SecurityGuardEmailTemplatePath { get { return ConfigurationManager.AppSettings["SecurityGuardEmailTemplatePath"]; } }
    }
}
