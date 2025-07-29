using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;
using Shared.Infrastructure.EFCore.Fluent;

namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseDiactivableEntityConfigurationExtension
    {
        public static void ConfigureDeactivable<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IDeactivableEntity
        {
            builder.Property(e => e.IsDeactive).UseBit();
            builder.Property(e => e.DeactiveDate).UsePersianDate();
            builder.Property(e => e.DeactiveTime).UseTime();
            builder.Property(e => e.DeactivatedByUserId).UseInt();
            builder.Property(e => e.DeactivatedByUserFullName).UseMediumString();
            builder.Property(e => e.DeactiveComment).UseMaxString();
        }
    }
}
