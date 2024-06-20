using DormModel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Composite primary key
            builder.HasKey(oi => new { oi.OrderId, oi.ItemId });

            // Relationships
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes if necessary

            builder.HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
