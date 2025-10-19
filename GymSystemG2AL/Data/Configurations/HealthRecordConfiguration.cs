using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystemG2AL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemG2AL.Data.Configurations
{
    internal class HealthRecordConfiguration
    {
        
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members");
            builder.HasOne<Member>()
                .WithOne(X => X.HealthRecord)
                .HasForeignKey<HealthRecord>(X => X.Id);

            builder.Ignore(X => X.CreatedAt);
            builder.Ignore(X => X.UpdatedAt);
        }
    }
}
