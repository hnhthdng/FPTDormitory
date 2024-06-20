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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Primary key
            builder.HasKey(p => p.Id);
            //builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100); // Adjust the length as needed

            // Relationships
            builder.HasMany(p => p.Transactions)
                .WithOne(t => t.Payment)
                .HasForeignKey(t => t.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.HasMany(p => p.Invoices)
                .WithOne(i => i.Payment)
                .HasForeignKey(i => i.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
