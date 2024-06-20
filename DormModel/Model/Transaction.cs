namespace DormModel.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Payment Payment { get; set; }
    }
}