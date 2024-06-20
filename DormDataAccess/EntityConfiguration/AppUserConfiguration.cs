using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DormDataAccess.EntityConfiguration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                // Table name
                builder.ToTable("AppUsers");

                // Configure one-to-one relationship with Balance
                builder.HasOne(e => e.Balance)
                      .WithOne(b => b.User)
                      .HasForeignKey<Balance>(b => b.UserId);

                // Configure one-to-many relationship with Orders
                builder.HasMany(e => e.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserId);


                // Configure one-to-many relationship with Invoices
                builder.HasMany(e => e.Invoices)
                      .WithOne(i => i.User)
                      .HasForeignKey(i => i.UserId);

            // Configure one-to-one relationship with Room
            builder.HasOne(u => u.Room)
                    .WithMany(r => r.AppUsers)
                    .HasForeignKey(u => u.RoomId)
                    .IsRequired(false) // Make RoomId nullable
                    .OnDelete(DeleteBehavior.SetNull); // SET NULL referential action
        }
        
    }
}
