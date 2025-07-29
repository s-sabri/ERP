using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shared.Infrastructure.EFCore.Fluent
{
    public static class FluentColumnExtensions
    {
        public static PropertyBuilder<string?> UseSmallString(this PropertyBuilder<string?> builder, bool required = false) =>
            builder.HasColumnType($"nvarchar(50)").IsRequired(required);

        public static PropertyBuilder<string?> UseMediumString(this PropertyBuilder<string?> builder, bool required = false) =>
            builder.HasColumnType($"nvarchar(100)").IsRequired(required);

        public static PropertyBuilder<string?> UseLargeString(this PropertyBuilder<string?> builder, bool required = false) =>
            builder.HasColumnType($"nvarchar(200)").IsRequired(required);

        public static PropertyBuilder<string?> UseMaxString(this PropertyBuilder<string?> builder, bool required = false) =>
            builder.HasColumnType("nvarchar(max)").IsRequired(required);

        public static PropertyBuilder<int?> UseInt(this PropertyBuilder<int?> builder, bool required = false) =>
            builder.HasColumnType("int").IsRequired(required);

        public static PropertyBuilder<long?> UseLong(this PropertyBuilder<long?> builder, bool required = false) =>
             builder.HasColumnType("long").IsRequired(required);

        public static PropertyBuilder<decimal?> UseDecimal(this PropertyBuilder<decimal?> builder, bool required = false) =>
            builder.HasColumnType("decimal(19, 4)").IsRequired(required);

        public static PropertyBuilder<bool> UseBit(this PropertyBuilder<bool> builder) =>
            builder.HasColumnType("bit").IsRequired();

        public static PropertyBuilder<string?> UsePersianDate(this PropertyBuilder<string?> builder, bool required = false) =>
            builder.HasColumnType("nvarchar(10)").IsRequired(required);

        public static PropertyBuilder<TimeSpan?> UseTime(this PropertyBuilder<TimeSpan?> builder, bool required = false) =>
            builder.HasColumnType("time(0)").IsRequired(required);

        public static PropertyBuilder<byte[]?> UseRowVersion(this PropertyBuilder<byte[]?> builder) =>
            builder.IsRowVersion().IsConcurrencyToken();
    }
}