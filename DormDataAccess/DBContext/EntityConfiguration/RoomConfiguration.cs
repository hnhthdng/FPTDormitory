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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100); // Adjust the length as needed

            // Relationships
            builder.HasMany(r => r.FloorRooms)
                .WithOne(fr => fr.Room)
                .HasForeignKey(fr => fr.RoomId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade deletes if necessary

            builder.HasMany(r => r.Floors)
            .WithMany(f => f.Rooms)
            .UsingEntity<FloorRoom>(
                j => j
                    .HasOne(fr => fr.Floor)
                    .WithMany(f => f.FloorRooms)
                    .HasForeignKey(fr => fr.FloorId),
                j => j
                    .HasOne(fr => fr.Room)
                    .WithMany(r => r.FloorRooms)
                    .HasForeignKey(fr => fr.RoomId),
                j =>
                {
                    j.HasKey(t => new { t.FloorId, t.RoomId });
                }
            );

            builder.HasMany(d => d.Orders)
            .WithMany(f => f.Rooms)
            .UsingEntity<RoomOrder>(
                j => j
                    .HasOne(df => df.Order)
                    .WithMany(f => f.RoomOrders)
                    .HasForeignKey(df => df.OrderId),
                j => j
                    .HasOne(df => df.Room)
                    .WithMany(d => d.RoomOrders)
                    .HasForeignKey(df => df.RoomId),
                j =>
                {
                    j.HasKey(t => new { t.OrderId, t.RoomId });
                }
            );
        }
    }
}
