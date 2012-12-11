using Mvc.Mailer;
using SG108Demo.Mailers.Models;

namespace SG108Demo.Mailers
{ 
    public interface IPasswordResetMailer
    {
			MvcMailMessage PasswordReset(MailerModel model);
	}
}