namespace Base.Caching.Services
{
	public interface IExternalCacheService
	{

		Task<R> ProcessAndGetAsync<R, T>(string key, T refClass, string methodName, object[] parameters = null, TimeSpan? absoluteExpireTime = null);

		Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
		Task<T> GetAsync<T>(string key);
		Task<bool> ContainsAsync(string key);
		Task RemoveAsync(string key);
		Task ClearAsync();

	}
}
