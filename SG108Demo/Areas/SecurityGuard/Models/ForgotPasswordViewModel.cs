using System.ComponentModel.DataAnnotations;

namespace SecurityGuard.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Display(Name = "Secret Question")]
        public string SecretQuestion { get; set; }

        [Display(Name = "Secret Answer")]
        public string SecretAnswer { get; set; }

        /// <summary>
        /// Indicates whether the RequiresQuestionAndAnswer was already
        /// checked in the ForgotPassword (POST) action method.  If so,
        /// we will skip over that checking "if" statement.
        /// </summary>
        public bool Checked { get; set; }

        public bool RequireSecretQuestionAndAnswer { get; set; }
    }
}
