using Base.Caching.Cachers;

namespace Base.Caching.Services
{
	public class MemoryCacheManager : IMemoryCacheService
	{

		#region CTOR

		private readonly IMemoryCache _memoryCache;

		public MemoryCacheManager(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		#endregion

		public void Set<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
		{
			_memoryCache.Set(key, data, absoluteExpireTime);
		}

		public T Get<T>(string key)
		{
			return _memoryCache.Get<T>(key);
		}

		public bool Contains(string key)
		{
			return _memoryCache.Contains(key);
		}

		public void Remove(string key)
		{
			_memoryCache.Remove(key);
		}

		public void Clear()
		{
			_memoryCache.Clear();
		}

	}
}
