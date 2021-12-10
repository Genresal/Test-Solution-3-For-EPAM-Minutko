using Microsoft.AspNetCore.Http;

namespace EMR.Models
{
    public class UsersFile
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
