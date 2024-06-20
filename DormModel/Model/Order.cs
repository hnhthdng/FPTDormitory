namespace DormModel.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; } = "pending";
        public string Description { get; set; }
        public float TotalPrice { get; set; }

        // Navigation properties
        public AppUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<OrderSideService> OrderSideServices { get; set; }
    }
}