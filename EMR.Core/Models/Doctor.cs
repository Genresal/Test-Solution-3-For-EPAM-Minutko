namespace EMR.Business.Models
{
    public class Doctor : BaseModel
    {
        public Doctor()
        {
            User = new User();
            Position = new Position();
        }
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public User User { get; set; }
        public Position Position { get; set; }
    }
}
