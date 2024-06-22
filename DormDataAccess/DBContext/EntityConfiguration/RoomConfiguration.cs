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

        }
    }
}
