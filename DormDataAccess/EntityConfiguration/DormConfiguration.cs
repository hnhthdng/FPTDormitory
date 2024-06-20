using DormModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.EntityConfiguration
{
    public class DormConfiguration : IEntityTypeConfiguration<Dorm>
    {
        public void Configure(EntityTypeBuilder<Dorm> builder)
        {
            // Configure primary key
            builder.HasKey(d => d.Id);
            //builder.Property(b => b.Id).UseIdentityColumn();

            // Configure properties
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure relationships
            builder.HasMany(d => d.DormFloors)
                .WithOne(df => df.Dorm)
                .HasForeignKey(df => df.DormId);
        }
    }
}
