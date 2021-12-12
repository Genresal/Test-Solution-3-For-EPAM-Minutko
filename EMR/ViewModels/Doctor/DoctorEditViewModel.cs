using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class DoctorEditViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Please select doctor's position")]
        [Display(Name = "First name")]
        public int PositionId { get; set; }
        public string Position { get; set; }
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

        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }

        public bool isUserAllowedToEdit { get; set; }
        public List<SelectListItem> Positions { get; set; }
    }
}
