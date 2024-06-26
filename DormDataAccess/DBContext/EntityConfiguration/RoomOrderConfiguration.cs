using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DBContext.EntityConfiguration
{
    public class RoomOrderConfiguration : IEntityTypeConfiguration<RoomOrder>
    {
        public void Configure(EntityTypeBuilder<RoomOrder> builder)
        {
            // Composite key
            builder.HasKey(oss => new { oss.OrderId, oss.RoomId });

            // Relationships
            builder.HasOne(oss => oss.Order)
                .WithMany(o => o.RoomOrders)
                .HasForeignKey(oss => oss.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes if necessary

            builder.HasOne(oss => oss.Room)
                .WithMany(ss => ss.RoomOrders)
                .HasForeignKey(oss => oss.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
