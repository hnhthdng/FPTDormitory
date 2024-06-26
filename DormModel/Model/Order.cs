namespace DormModel.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string Description { get; set; }
        public float TotalPrice { get; set; }

        // Navigation properties
        public AppUser User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<OrderSideService> OrderSideServices { get; set; }
        public virtual ICollection<SideService> SideServices { get; set; }

        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}