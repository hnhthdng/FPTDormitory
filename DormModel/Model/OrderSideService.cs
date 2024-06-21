namespace DormModel.Model
{
    public class OrderSideService
    {
        public int OrderId { get; set; }
        public int SideServiceId { get; set; }

        public virtual Order Order { get; set; }
        public virtual SideService SideService { get; set; }
    }
}