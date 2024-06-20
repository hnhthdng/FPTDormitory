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
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            // Configure primary key
            builder.HasKey(b => b.Id);
            //builder.Property(b => b.Id).UseIdentityColumn();

            // Configure properties
            builder.Property(b => b.Amount)
                .IsRequired()
                .HasDefaultValue(0);

        }
    }
}
