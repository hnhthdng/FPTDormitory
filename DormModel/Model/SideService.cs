namespace DormModel.Model
{
    public class SideService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public virtual ICollection<OrderSideService> OrderSideServices { get; set; }
    }
}