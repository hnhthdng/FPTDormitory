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
        }
    }
}
