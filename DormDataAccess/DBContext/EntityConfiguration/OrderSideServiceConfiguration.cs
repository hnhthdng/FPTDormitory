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
    public class OrderSideServiceConfiguration : IEntityTypeConfiguration<OrderSideService>
    {
        public void Configure(EntityTypeBuilder<OrderSideService> builder)
        {
            // Composite key
            builder.HasKey(oss => new { oss.OrderId, oss.SideServiceId });

            // Relationships
            builder.HasOne(oss => oss.Order)
                .WithMany(o => o.OrderSideServices)
                .HasForeignKey(oss => oss.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes if necessary

            builder.HasOne(oss => oss.SideService)
                .WithMany(ss => ss.OrderSideServices)
                .HasForeignKey(oss => oss.SideServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
