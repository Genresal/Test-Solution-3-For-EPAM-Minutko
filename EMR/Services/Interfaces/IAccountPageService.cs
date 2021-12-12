using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EMR.Services
{
    public interface IAccountPageService
    {
        public bool IsLoginExist(string login);
        public List<SelectListItem> PrepareRoles();
    }
}
