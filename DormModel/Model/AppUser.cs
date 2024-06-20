using Microsoft.AspNetCore.Identity;

namespace DormModel.Model
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }

        public virtual Balance? Balance { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}