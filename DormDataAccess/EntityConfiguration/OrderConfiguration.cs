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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary key
            builder.HasKey(o => o.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(50); // Adjust the length as needed

            builder.Property(o => o.Description)
                .HasMaxLength(1000); // Adjust the length as needed

            // Relationships
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes for OrderItem if necessary

            builder.HasMany(o => o.Transactions)
                .WithOne(t => t.Order)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.HasMany(o => o.OrderSideServices)
                .WithOne(oss => oss.Order)
                .HasForeignKey(oss => oss.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes for OrderSideService if necessary
        }
    }
}
