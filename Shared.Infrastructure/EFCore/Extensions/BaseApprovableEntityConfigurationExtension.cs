using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseApprovableEntityConfigurationExtension
    {
        public static void ConfigureApprovable1<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IApprovable1Entity
        {
            builder.Property(e => e.IsApproved1)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.Approve1Date)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.Approve1Time)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.Approved1ByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.Approved1ByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.Approve1Comment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
        public static void ConfigureApprovable2<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IApprovable2Entity
        {
            builder.Property(e => e.IsApproved2)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.Approve2Date)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.Approve2Time)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.Approved2ByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.Approved2ByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.Approve2Comment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
        public static void ConfigureApprovable3<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IApprovable3Entity
        {
            builder.Property(e => e.IsApproved3)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.Approve3Date)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.Approve3Time)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.Approved3ByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.Approved3ByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.Approve3Comment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
        public static void ConfigureApprovable4<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IApprovable4Entity
        {
            builder.Property(e => e.IsApproved4)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(e => e.Approve4Date)
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(e => e.Approve4Time)
                .HasColumnType("time(0)")
                .IsRequired(false);

            builder.Property(e => e.Approved4ByUserId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(e => e.Approved4ByUserFullName)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder.Property(e => e.Approve4Comment)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }
}
