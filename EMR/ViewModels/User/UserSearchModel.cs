using EMR.DataTables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMR.ViewModels
{
    public class UserSearchModel : DataTablesParameters
    {
        public UserSearchModel()
        {
            Roles = new List<FilterCondition>();
        }

        [Display(Name = "Search by name")]
        public string FullName { get; set; }

        [Display(Name = "User roles")]
        public List<FilterCondition> Roles { get; set; }
        public bool isUserAdmin { get; set; }
    }
}
