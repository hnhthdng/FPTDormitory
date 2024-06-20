using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormModel.Model;

namespace DormDataAccess.EntityConfiguration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Primary key
            builder.HasKey(t => t.Id);
            //builder.Property(b => b.Id).UseIdentityColumn();

            // Properties
            builder.Property(t => t.Status)
                .HasMaxLength(50); // Adjust the length as needed


            builder.HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.HasOne(t => t.Payment)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
