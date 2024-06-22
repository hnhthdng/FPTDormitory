using DormModel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DBContext.EntityConfiguration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            // Primary key
            builder.HasKey(i => i.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(i => i.Status)
                .IsRequired()
                .HasMaxLength(50); // Adjust the length as needed

            builder.HasMany(i => i.OrderItems)
                .WithOne(oi => oi.Item)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes for OrderItem if necessary
        }
    }
}
