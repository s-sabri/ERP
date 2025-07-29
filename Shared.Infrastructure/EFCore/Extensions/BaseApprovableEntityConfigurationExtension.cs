using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Abstractions.Behaviors;
using Shared.Infrastructure.EFCore.Fluent;

namespace Shared.Infrastructure.EFCore.Extensions
{
    public static class BaseApprovableEntityConfigurationExtension
    {
        public static void ConfigureApprovable1<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IApprovable1Entity
        {
            builder.Property(e => e.IsApproved1).UseBit();
            builder.Property(e => e.Approve1Date).UsePersianDate();
            builder.Property(e => e.Approve1Time).UseTime();
            builder.Property(e => e.Approved1ByUserId).UseInt();
            builder.Property(e => e.Approved1ByUserFullName).UseMediumString();
            builder.Property(e => e.Approve1Comment).UseMaxString();
        }
        public static void ConfigureApprovable2<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IApprovable2Entity
        {
            builder.Property(e => e.IsApproved2).UseBit();
            builder.Property(e => e.Approve2Date).UsePersianDate();
            builder.Property(e => e.Approve2Time).UseTime();
            builder.Property(e => e.Approved2ByUserId).UseInt();
            builder.Property(e => e.Approved2ByUserFullName).UseMediumString();
            builder.Property(e => e.Approve2Comment).UseMaxString();
        }
        public static void ConfigureApprovable3<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IApprovable3Entity
        {
            builder.Property(e => e.IsApproved3).UseBit();
            builder.Property(e => e.Approve3Date).UsePersianDate();
            builder.Property(e => e.Approve3Time).UseTime();
            builder.Property(e => e.Approved3ByUserId).UseInt();
            builder.Property(e => e.Approved3ByUserFullName).UseMediumString();
            builder.Property(e => e.Approve3Comment).UseMaxString();
        }
        public static void ConfigureApprovable4<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IApprovable4Entity
        {
            builder.Property(e => e.IsApproved4).UseBit();
            builder.Property(e => e.Approve4Date).UsePersianDate();
            builder.Property(e => e.Approve4Time).UseTime();
            builder.Property(e => e.Approved4ByUserId).UseInt();
            builder.Property(e => e.Approved4ByUserFullName).UseMediumString();
            builder.Property(e => e.Approve4Comment).UseMaxString();
        }
    }
}
