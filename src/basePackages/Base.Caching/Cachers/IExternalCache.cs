namespace Base.Caching.Cachers
{
	public interface IExternalCache
	{

		Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
		Task<T> GetAsync<T>(string key);
		Task RemoveAsync(string key);
		Task<bool> ContainsAsync(string key);
		Task ClearAsync();

	}
}
