using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Fluent;
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

            builder.Property(e => e.OldValues).UseMaxString();
            builder.Property(e => e.NewValues).UseMaxString();
            builder.Property(e => e.Changes).UseMaxString();
            builder.Property(e => e.Exception).UseMaxString();
        }
    }
}
