using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Fluent;
using Shared.Infrastructure.Logging.Entities;

namespace Shared.Infrastructure.EFCore.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs", schema: "Log");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetime2(3)")
                .IsRequired();

            builder.Property(e => e.UserAgent)
                .HasColumnType("nvarchar(1024)");

            builder.Property(e => e.UserId).UseSmallString();
            builder.Property(e => e.UserFullName).UseLargeString();
            builder.Property(e => e.IpAddress).UseSmallString();
            builder.Property(e => e.Module).UseMediumString();
            builder.Property(e => e.SubSystem).UseMediumString();
            builder.Property(e => e.Action).UseMediumString();
            builder.Property(e => e.EntityName).UseMediumString();
            builder.Property(e => e.EntityId).UseMediumString();
            builder.Property(e => e.Status).UseSmallString();
            builder.Property(e => e.CorrelationId).UseMediumString();
            builder.Property(e => e.TransactionId).UseMediumString();
            builder.HasIndex(e => e.CreatedAt);
            builder.HasIndex(e => e.Module);
            builder.HasIndex(e => e.CorrelationId);
            builder.HasIndex(e => e.UserId);

            builder.HasOne(e => e.Detail)
                .WithOne(d => d.AuditLog)
                .HasForeignKey<AuditLogDetail>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
