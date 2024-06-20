namespace DormModel.Model
{
    public class Balance
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public float Amount { get; set; } = 0;

        //  Navigation properties
        public virtual AppUser? User { get; set; }
    }
}