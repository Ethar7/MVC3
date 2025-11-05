// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using GymSystemG2AL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace GymSystemG2AL.Data.Configurations
// {
//     internal class MemberConfiguration : GymUserConfiguration<Member> , IEntityTypeConfiguration<Member>
//     {
//         public new void Configure(EntityTypeBuilder<Member> builder)
//         {
//             // special Type JoinDate
//             builder.Property(X => X.CreatedAt)
//                 .HasColumnName("JoinDate")
//                 .HasDefaultValueSql("GETDATE()");

//             base.Configure(builder);
//         }
//     }
// }

// using GymSystemG2AL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace GymSystemG2AL.Data.Configurations
// {
//     internal class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
//     {
//         public override void Configure(EntityTypeBuilder<Member> builder)
//         {
         
//             builder.Property(x => x.CreatedAt)
//                    .HasColumnName("JoinDate")
//                    .HasDefaultValueSql("GETDATE()");

           
//             builder.Ignore(x => x.UpdatedAt);

          
//             base.Configure(builder);

            
//             builder.HasIndex(x => x.Email).IsUnique();
//             builder.HasIndex(x => x.Phone).IsUnique();

//         }
//     }
// }


using GymSystemG2AL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemG2AL.Data.Configurations
{
    internal class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Table name
            builder.ToTable("Members");

            // Primary key
            builder.HasKey(m => m.Id);

            // Map CreatedAt as JoinDate
            builder.Property(m => m.CreatedAt)
                   .HasColumnName("JoinDate")
                   .HasDefaultValueSql("GETDATE()");

            // Properties
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(m => m.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(m => m.Phone)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.Property(m => m.DateOfBirth)
                   .IsRequired();

            builder.Property(m => m.Gender)
                   .IsRequired();

            // Owned type for Address
            builder.OwnsOne(m => m.Address, a =>
            {
                a.Property(x => x.BuildingNumber).HasColumnName("Address_BuildingNumber");
                a.Property(x => x.Street).HasMaxLength(30);
                a.Property(x => x.City).HasMaxLength(30);
            });

            // Unique constraints
            builder.HasIndex(m => m.Email).IsUnique();
            builder.HasIndex(m => m.Phone).IsUnique();

            // One-to-One with HealthRecord
            builder.HasOne(m => m.HealthRecord)
                   .WithOne(hr => hr.Member)
                   .HasForeignKey<HealthRecord>(hr => hr.MemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Ignore UpdatedAt if not needed
            builder.Ignore(m => m.UpdatedAt);
            builder.Ignore(m => m.CreatedAt);
        }
    }
}

