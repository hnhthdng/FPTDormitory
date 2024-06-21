namespace DormModel.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Payment Payment { get; set; }
    }
}