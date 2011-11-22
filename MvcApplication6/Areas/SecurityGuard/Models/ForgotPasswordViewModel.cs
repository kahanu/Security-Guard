using System.ComponentModel.DataAnnotations;

namespace SecurityGuard.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage="Email is required.")]
        public string Email { get; set; }

        public string PasswordAnswer { get; set; }

        public bool RequiresQuestionAndAnswer { get; set; }
    }
}
