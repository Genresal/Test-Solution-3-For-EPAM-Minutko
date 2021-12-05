using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Login..")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please Choose User Role...")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
