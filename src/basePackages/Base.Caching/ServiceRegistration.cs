using Base.Caching.Cachers;
using Base.Caching.Pipelines;
using Base.Caching.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Caching
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddCacheServices(this IServiceCollection services)
		{
			services.AddMemoryCache();

			//services.AddSingleton<IExternalCache>(provider => new RedisCacher(connectionString, productIdentifier, envIdentifier));
			services.AddSingleton<IMemoryCache, MemoryCacher>();

			services.AddSingleton<IMemoryCacheService, MemoryCacheManager>();
			services.AddSingleton<IExternalCacheService, ExternalCacheManager>();

			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));

			return services;
		}
	}
}
