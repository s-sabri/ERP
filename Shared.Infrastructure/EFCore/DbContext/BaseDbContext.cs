using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shared.Domain.Entities;
using Shared.Infrastructure.EFCore.Configurations;

namespace Shared.Infrastructure.EFCore.DbContext
{
    public class BaseDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogDetailConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ApplyGlobalFilters(modelBuilder, entityType);
                ApplyConcurrencyTokens(modelBuilder, entityType);
            }
        }

        private void ApplyGlobalFilters(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            if (entityType.ClrType.BaseType != null && typeof(BaseAuditableEntity<>).IsAssignableFrom(entityType.ClrType.BaseType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyMethodInfo = typeof(EF).GetMethod("Property")!
                    .MakeGenericMethod(typeof(bool));
                var isDeletedProperty =
                    Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
                var compareExpression = Expression.Equal(isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
        private void ApplyConcurrencyTokens(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            var rowVersionProperty = entityType
                .GetProperties()
                .FirstOrDefault(p => p.Name == "RowVersion");

            if (rowVersionProperty != null)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<byte[]>("RowVersion")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            }
        }
    }
}
