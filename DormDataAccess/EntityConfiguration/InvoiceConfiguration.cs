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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // Configure primary key
            builder.HasKey(i => i.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            // Configure properties
            builder.Property(i => i.UserFullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.TotalPrice)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Description)
                .HasMaxLength(1000);

            builder.Property(i => i.CreateDate)
                .IsRequired();

            builder.Property(i => i.Status)
                .IsRequired()
                .HasMaxLength(50);

            // Configure relationships
            builder.HasOne(i => i.Transaction)
                .WithMany()
                .HasForeignKey(i => i.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Payment)
                .WithMany()
                .HasForeignKey(i => i.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
