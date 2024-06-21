namespace DormModel.Model
{
    public class Item
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string Status { get; set; } = "pending";
        public float Price { get; set; }

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}