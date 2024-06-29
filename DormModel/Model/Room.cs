namespace DormModel.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaximumNumberOfPeople { get; set; }
        public int CurrentNumberOfPeople { get; set; }
        public bool IsMaximum { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<FloorRoom> FloorRooms { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }
}