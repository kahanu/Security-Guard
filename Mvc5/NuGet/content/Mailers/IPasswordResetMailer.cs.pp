using Mvc.Mailer;
using $rootnamespace$.Mailers.Models;

namespace $rootnamespace$.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}