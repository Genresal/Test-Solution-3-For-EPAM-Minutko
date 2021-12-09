using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your login")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Message { get; set; }
    }
}
