using System;
using System.Linq;

namespace $rootnamespace$.Mailers.Models
{
    public class MailerModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
    }
}