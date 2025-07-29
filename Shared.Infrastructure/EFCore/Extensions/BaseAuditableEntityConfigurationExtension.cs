using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EFCore.Fluent;


namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseAuditableEntityConfigurationExtension
    {
        public static void ConfigureBaseAuditableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IAuditableEntity
        {
            builder.Property(e => e.CreateDate).UsePersianDate();
            builder.Property(e => e.CreateTime).UseTime();
            builder.Property(e => e.CreatedByUserId).UseInt();
            builder.Property(e => e.CreatedByUserFullName).UseMediumString();

            builder.Property(e => e.UpdateDate).UsePersianDate();
            builder.Property(e => e.UpdateTime).UseTime();
            builder.Property(e => e.UpdatedByUserId).UseInt();
            builder.Property(e => e.UpdatedByUserFullName).UseMediumString();
            builder.Property(e => e.UpdateComment).UseMaxString();

            builder.Property(e => e.IsDeleted).UseBit();
            builder.Property(e => e.DeleteDate).UsePersianDate();
            builder.Property(e => e.DeleteTime).UseTime();
            builder.Property(e => e.DeletedByUserId).UseInt();
            builder.Property(e => e.DeletedByUserFullName).UseMediumString();
            builder.Property(e => e.DeleteComment).UseMaxString();

            builder.Property(e => e.RowVersion).UseRowVersion();
        }
    }
}
