namespace EMR.Business.Models
{
    public class Patient : BaseModel
    {
        public Patient()
        {
            User = new User();
        }

        public int UserId { get; set; }
        public string Job { get; set; }
        public User User { get; set; }
    }
}
