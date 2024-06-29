using Microsoft.AspNetCore.Identity;

namespace DormModel.Model
{
    public class AppUser : IdentityUser
    {
        public int? RoomId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual Room Room { get; set; }
    }
}