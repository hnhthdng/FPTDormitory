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
    public class FloorConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            builder.Property(f => f.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(f => f.DormFloors)
                .WithOne(df => df.Floor)
                .HasForeignKey(df => df.FloorId);

            builder.HasMany(f => f.FloorRooms)
                .WithOne(fr => fr.Floor)
                .HasForeignKey(fr => fr.FloorId);

            // Optionally configure the relationship with Dorm if needed
            builder.HasMany(f => f.Dorms)
                .WithMany(d => d.Floors)
                .UsingEntity<DormFloor>(
                    j => j
                        .HasOne(df => df.Dorm)
                        .WithMany(d => d.DormFloors)
                        .HasForeignKey(df => df.DormId),
                    j => j
                        .HasOne(df => df.Floor)
                        .WithMany(f => f.DormFloors)
                        .HasForeignKey(df => df.FloorId),
                    j =>
                    {
                        j.HasKey(t => new { t.DormId, t.FloorId });
                    }
                );

            builder.HasMany(f => f.Rooms)
            .WithMany(r => r.Floors)
            .UsingEntity<FloorRoom>(
                j => j
                    .HasOne(fr => fr.Room)
                    .WithMany(r => r.FloorRooms)
                    .HasForeignKey(fr => fr.RoomId),
                j => j
                    .HasOne(fr => fr.Floor)
                    .WithMany(f => f.FloorRooms)
                    .HasForeignKey(fr => fr.FloorId),
                j =>
                {
                    j.HasKey(t => new { t.FloorId, t.RoomId });
                }
            );
        }
    }
}
