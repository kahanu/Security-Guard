using System.ComponentModel.DataAnnotations;

namespace SecurityGuard.ViewModels
{
    public partial class LogOnViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool EnablePasswordReset { get; set; }
    }
}
