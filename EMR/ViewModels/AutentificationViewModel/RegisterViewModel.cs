using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter login")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please choose user role")]
        [Display(Name = "User role")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter the confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Message { get; set; }
    }
}
