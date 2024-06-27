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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Primary key
            builder.HasKey(o => o.Id);

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


            builder.HasMany(o => o.OrderSideServices)
                .WithOne(oss => oss.Order)
                .HasForeignKey(oss => oss.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes for OrderSideService if necessary

            builder.HasMany(d => d.SideServices)
            .WithMany(f => f.Orders)
            .UsingEntity<OrderSideService>(
                j => j
                    .HasOne(df => df.SideService)
                    .WithMany(f => f.OrderSideServices)
                    .HasForeignKey(df => df.SideServiceId),
                j => j
                    .HasOne(df => df.Order)
                    .WithMany(d => d.OrderSideServices)
                    .HasForeignKey(df => df.OrderId),
                j =>
                {
                    j.HasKey(t => new { t.OrderId, t.SideServiceId });
                }
            );

            builder.HasMany(d => d.Rooms)
            .WithMany(f => f.Orders)
            .UsingEntity<RoomOrder>(
                j => j
                    .HasOne(df => df.Room)
                    .WithMany(f => f.RoomOrders)
                    .HasForeignKey(df => df.RoomId),
                j => j
                    .HasOne(df => df.Order)
                    .WithMany(d => d.RoomOrders)
                    .HasForeignKey(df => df.OrderId),
                j =>
                {
                    j.HasKey(t => new { t.OrderId, t.RoomId });
                }
            );
        }
    }
}
