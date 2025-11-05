// using GymSystemG2AL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace GymSystemG2AL.Data.Configurations
// {
//     internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
//     {
//         public void Configure(EntityTypeBuilder<HealthRecord> builder)
//         {
//             builder.ToTable("HealthRecords");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Height)
//                    .HasPrecision(5, 2); // لتجنب التحذيرات الخاصة بـ decimal

//             builder.Property(x => x.Weight)
//                    .HasPrecision(5, 2);

//             // العلاقة One-to-One
//             builder.HasOne(x => x.Member)
//                    .WithOne(x => x.HealthRecord)
//                    .HasForeignKey<HealthRecord>(x => x.MemberId)
//                    .OnDelete(DeleteBehavior.Cascade);

//             // تجاهل الحقول الغير ضرورية لو كانت موجودة في BaseEntity
//             builder.Ignore(x => x.CreatedAt);
//             builder.Ignore(x => x.UpdatedAt);
//         }
//     }
// }

// using GymSystemG2AL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace GymSystemG2AL.Data.Configurations
// {
//     internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
//     {
//         public void Configure(EntityTypeBuilder<HealthRecord> builder)
//         {
//             builder.ToTable("HealthRecords");

//             builder.HasKey(x => x.Id);

//             builder.Property(x => x.Height)
//                    .HasPrecision(5, 2);

//             builder.Property(x => x.Weight)
//                    .HasPrecision(5, 2);

            
//             builder.HasOne(x => x.Member)
//                    .WithOne(x => x.HealthRecord)
//                    .HasForeignKey<HealthRecord>(x => x.MemberId)
//                    .OnDelete(DeleteBehavior.Cascade);

//             builder.Ignore(x => x.CreatedAt);
//             builder.Ignore(x => x.UpdatedAt);
//         }
//     }
// }


using GymSystemG2AL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemG2AL.Data.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            // Table name
            builder.ToTable("HealthRecords");

            // Primary key
            builder.HasKey(hr => hr.Id);

            // Properties with precision for decimals
            builder.Property(hr => hr.Height)
                   .HasPrecision(5, 2)
                   .IsRequired();

            builder.Property(hr => hr.Weight)
                   .HasPrecision(5, 2)
                   .IsRequired();

            builder.Property(hr => hr.BloodType)
                   .IsRequired();

            builder.Property(hr => hr.Note);

            // One-to-One with Member
            builder.HasOne(hr => hr.Member)
                   .WithOne(m => m.HealthRecord)
                   .HasForeignKey<HealthRecord>(hr => hr.MemberId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Ignore BaseEntity fields if not needed
            builder.Ignore(hr => hr.CreatedAt);
            builder.Ignore(hr => hr.UpdatedAt);
        }
    }
}

