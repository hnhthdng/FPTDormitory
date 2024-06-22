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
    public class DormFloorConfiguration : IEntityTypeConfiguration<DormFloor>
    {
        public void Configure(EntityTypeBuilder<DormFloor> builder)
        {
            // Configure composite key
            builder.HasKey(df => new { df.DormId, df.FloorId });

            // Configure relationships
            builder.HasOne(df => df.Dorm)
                .WithMany(d => d.DormFloors)
                .HasForeignKey(df => df.DormId);

            builder.HasOne(df => df.Floor)
                .WithMany(f => f.DormFloors)
                .HasForeignKey(df => df.FloorId);
        }
    }
}
