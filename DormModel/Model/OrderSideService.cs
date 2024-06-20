namespace DormModel.Model
{
    public class OrderSideService
    {
        public Guid OrderId { get; set; }
        public Guid SideServiceId { get; set; }

        public virtual Order Order { get; set; }
        public virtual SideService SideService { get; set; }
    }
}