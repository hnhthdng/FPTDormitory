namespace DormModel.Model
{
    public class Balance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public float Amount { get; set; } = 0;

        //  Navigation properties
        public virtual AppUser User { get; set; }
    }
}