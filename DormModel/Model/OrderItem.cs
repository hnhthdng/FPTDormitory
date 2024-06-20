namespace DormModel.Model
{
    public class OrderItem
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}