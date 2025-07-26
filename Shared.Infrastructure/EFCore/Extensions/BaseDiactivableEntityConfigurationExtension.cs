using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseDiactivableEntityConfigurationExtension
    {
        public static void ConfigureDeactivable<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IDeactivableEntity
        {
            builder.Property(e => e.IsDeactive)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.DeactiveDate)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.DeactiveTime)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.DeactivatedByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.DeactivatedByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.DeactiveComment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }
}
