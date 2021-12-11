using System;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please select date of birth")]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter job")]
        [Display(Name = "Job")]
        public string Job { get; set; }

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        public string FullName { get; set; }

        public bool isUserAllowedToEdit { get; set; }
    }
}
