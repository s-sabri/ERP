using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Abstractions.Specifications;
using Shared.Application.Contracts.Services;
using Shared.Infrastructure.EFCore.Specifications;

namespace Shared.Infrastructure.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSpecificationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IQuerySpecificationEvaluator<>), typeof(EfCoreQuerySpecificationEvaluator<>));
            services.AddScoped(typeof(IQueryableService<>), typeof(EfCoreQueryableService<>));

            return services;
        }
    }
}
