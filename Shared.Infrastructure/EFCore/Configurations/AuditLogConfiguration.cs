using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            builder.Property(e => e.UserId)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.UserFullName)
                .HasColumnType("nvarchar(256)");

            builder.Property(e => e.IpAddress)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.UserAgent)
                .HasColumnType("nvarchar(1024)");

            builder.Property(e => e.Module)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.SubSystem)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Action)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.EntityName)
                .HasColumnType("nvarchar(128)");

            builder.Property(e => e.EntityId)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Status)
                .HasColumnType("nvarchar(50)");

            builder.Property(e => e.CorrelationId)
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.TransactionId)
                .HasColumnType("nvarchar(100)");

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
