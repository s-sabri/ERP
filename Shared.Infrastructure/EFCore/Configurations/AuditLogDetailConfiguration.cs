using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Logging.Entities;

namespace Shared.Infrastructure.EFCore.Configurations
{
    public class AuditLogDetailConfiguration : IEntityTypeConfiguration<AuditLogDetail>
    {
        public void Configure(EntityTypeBuilder<AuditLogDetail> builder)
        {
            builder.ToTable("AuditLogDetails", schema: "Log");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.OldValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.NewValues)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Changes)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Exception)
                .HasColumnType("nvarchar(max)");
        }
    }
}
