namespace DormModel.Model
{
    public class SideService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<OrderSideService> OrderSideServices { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}