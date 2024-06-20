﻿using DormModel.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.EntityConfiguration
{
    public class SideServiceConfiguration : IEntityTypeConfiguration<SideService>
    {
        public void Configure(EntityTypeBuilder<SideService> builder)
        {
            // Primary key
            builder.HasKey(ss => ss.Id);
            //builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(ss => ss.Name)
                .IsRequired()
                .HasMaxLength(100); // Adjust the length as needed

            builder.Property(ss => ss.Description)
                .HasMaxLength(500); // Adjust the length as needed

            // Relationships
            builder.HasMany(ss => ss.OrderSideServices)
                .WithOne(oss => oss.SideService)
                .HasForeignKey(oss => oss.SideServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes if necessary
        }
    }
}
