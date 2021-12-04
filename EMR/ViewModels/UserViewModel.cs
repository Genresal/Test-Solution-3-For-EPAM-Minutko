using EMR.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
    }
}
