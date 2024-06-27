using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DormDataAccess.DBContext.EntityConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Table name
            builder.ToTable("AppUsers");


            // Configure one-to-many relationship with Orders
            builder.HasMany(e => e.Orders)
                  .WithOne(o => o.User)
                  .HasForeignKey(o => o.UserId);



            // Configure one-to-one relationship with Room
            builder.HasOne(u => u.Room)
                    .WithMany(r => r.AppUsers)
                    .HasForeignKey(u => u.RoomId)
                    .IsRequired(false) // Make RoomId nullable
                    .OnDelete(DeleteBehavior.SetNull); // SET NULL referential action
        }

    }
}
