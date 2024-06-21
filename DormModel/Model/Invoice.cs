namespace DormModel.Model
{
    public class Invoice
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public int PaymentId { get; set; }
        public string UserFullName { get; set; }
        public string TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Transaction Transaction { get; set; }

        public AppUser User { get; set; }
        public Payment Payment { get; set; }
    }
}