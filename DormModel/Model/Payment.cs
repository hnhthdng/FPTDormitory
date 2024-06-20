namespace DormModel.Model
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}