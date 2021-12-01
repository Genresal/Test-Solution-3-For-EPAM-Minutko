using EMR.DataTables;
using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        public int RoleId { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
