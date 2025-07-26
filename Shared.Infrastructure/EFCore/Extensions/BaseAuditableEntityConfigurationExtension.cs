using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;
using Microsoft.EntityFrameworkCore;


namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseAuditableEntityConfigurationExtension
    {
        public static void ConfigureBaseAuditableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IAuditableEntity
        {
            builder.Property(e => e.CreateDate)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.CreateTime)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.CreatedByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.CreatedByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);


            builder.Property(e => e.UpdateDate)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.UpdateTime)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.UpdatedByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.UpdatedByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.UpdateComment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);


            builder.Property(e => e.IsDeleted)
                .HasColumnType("bit").IsRequired();

            builder.Property(e => e.DeleteDate)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.DeleteTime)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.DeletedByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.DeletedByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.DeleteComment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);


            builder.Property(e => e.RowVersion).IsRowVersion().IsConcurrencyToken();
        }
    }
}
