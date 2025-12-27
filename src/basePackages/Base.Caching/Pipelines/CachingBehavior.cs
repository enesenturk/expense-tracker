using Base.Caching.Services;
using Base.Exceptions.ExceptionModels;
using MediatR;

namespace Base.Caching.Pipelines
{
	public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>, ICachableRequest
	{

		#region CTOR

		private readonly IMemoryCacheService _memoryCacheService;

		public CachingBehavior(IMemoryCacheService memoryCacheService)
		{
			_memoryCacheService = memoryCacheService;
		}

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (request.IsBypassCache)
				return await next();

			if (request.CacheKey != null && request.CacheKeyParameters != "")
				throw new AbsurdOperationException("CacheKey and CacheKeyParameters can not be provided at the same time. Use the CacheKeyParameters inside the CacheKey.");

			string cacheKey = request.CacheKey ??
							$"{typeof(TRequest).Name}_{request.CacheKeyParameters}";

			TResponse cachedResponse = _memoryCacheService.Get<TResponse>(cacheKey);

			TResponse response = cachedResponse ??
				await GetResponseAndAddToCache(cacheKey, request, cancellationToken, next);

			return response;
		}

		#region Behind the Scenes

		private async Task<TResponse> GetResponseAndAddToCache(string cacheKey, TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			TResponse response = await next();

			_memoryCacheService.Set(cacheKey, response, request.CacheExpiration);

			return response;
		}

		#endregion

	}
}
