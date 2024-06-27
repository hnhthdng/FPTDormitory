namespace DormModel.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string Description { get; set; }
        public float TotalPrice { get; set; }

        // Navigation properties
        public virtual AppUser User { get; set; }

        public virtual ICollection<OrderSideService> OrderSideServices { get; set; }
        public virtual ICollection<SideService> SideServices { get; set; }

        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}