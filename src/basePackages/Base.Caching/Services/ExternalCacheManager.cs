using Base.Caching.Cachers;
using System.Reflection;

namespace Base.Caching.Services
{
	public class ExternalCacheManager : IExternalCacheService
	{

		#region CTOR

		private readonly IExternalCache _externalCache;

		public ExternalCacheManager(IExternalCache externalCache)
		{
			_externalCache = externalCache;
		}

		#endregion

		public async Task<R> ProcessAndGetAsync<R, T>(string key, T refClass, string methodName, object[] parameters = null, TimeSpan? absoluteExpireTime = null)
		{
			bool objHasCached = await _externalCache.ContainsAsync(key);

			if (objHasCached)
				return await GetByCaching<R>(key);

			R result = GetByCalling<R, T>(refClass, methodName, parameters);

			if (result == null)
				return result;

			await SetToCache(key, result, absoluteExpireTime);

			return result;
		}

		public async Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
		{
			await _externalCache.SetAsync(key, data, absoluteExpireTime);
		}

		public async Task<T> GetAsync<T>(string key)
		{
			T data = await _externalCache.GetAsync<T>(key);

			return data;
		}

		public async Task<bool> ContainsAsync(string key)
		{
			bool contains = await _externalCache.ContainsAsync(key);

			return contains;
		}

		public async Task RemoveAsync(string key)
		{
			await _externalCache.RemoveAsync(key);
		}

		public async Task ClearAsync()
		{
			await _externalCache.ClearAsync();
		}

		#region Refactor

		private async Task SetToCache<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
		{
			await _externalCache.SetAsync(key, data, absoluteExpireTime);
		}

		private Task<R> GetByCaching<R>(string key)
		{
			return _externalCache.GetAsync<R>(key);
		}

		private R GetByCalling<R, T>(T refClass, string methodName, object[] parameters = null)
		{
			Type refType = refClass.GetType();

			MethodInfo refMethod = refType.GetMethod(methodName);
			object result = refMethod.Invoke(refClass, parameters);

			return (R)result;
		}

		#endregion

	}
}
