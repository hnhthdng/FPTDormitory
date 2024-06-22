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
    public class FloorRoomConfiguration : IEntityTypeConfiguration<FloorRoom>
    {
        public void Configure(EntityTypeBuilder<FloorRoom> builder)
        {
            // Configure composite key
            builder.HasKey(fr => new { fr.FloorId, fr.RoomId });

            // Configure relationships
            builder.HasOne(fr => fr.Floor)
                .WithMany(f => f.FloorRooms)
                .HasForeignKey(fr => fr.FloorId);

            builder.HasOne(fr => fr.Room)
                .WithMany(r => r.FloorRooms)
                .HasForeignKey(fr => fr.RoomId);
        }
    }
}
