namespace DormModel.Model
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public string Status { get; set; } = "pending";
        public float Price { get; set; }

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}