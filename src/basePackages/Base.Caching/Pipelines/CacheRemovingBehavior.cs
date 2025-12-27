using Base.Caching.Services;
using MediatR;

namespace Base.Caching.Pipelines
{
	public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>, ICacheRemoverRequest
	{

		#region CTOR

		private readonly IMemoryCacheService _memoryCacheService;

		public CacheRemovingBehavior(IMemoryCacheService memoryCacheService)
		{
			_memoryCacheService = memoryCacheService;
		}

		#endregion

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (request.IsBypassCache)
				return await next();

			TResponse response = await next();

			foreach (string cacheKey in request.CacheKeys)
				_memoryCacheService.Remove(cacheKey);

			return response;
		}

	}
}
