using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DBContext.EntityConfiguration
{
    public class DormConfiguration : IEntityTypeConfiguration<Dorm>
    {
        public void Configure(EntityTypeBuilder<Dorm> builder)
        {
            // Configure primary key
            builder.HasKey(d => d.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            // Configure properties
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure relationships
           
            builder.HasMany(d => d.Floors)
            .WithMany(f => f.Dorms)
            .UsingEntity<DormFloor>(
                j => j
                    .HasOne(df => df.Floor)
                    .WithMany(f => f.DormFloors)
                    .HasForeignKey(df => df.FloorId),
                j => j
                    .HasOne(df => df.Dorm)
                    .WithMany(d => d.DormFloors)
                    .HasForeignKey(df => df.DormId),
                j =>
                {
                    j.HasKey(t => new { t.DormId, t.FloorId });
                }
            );
        }
    }
}
