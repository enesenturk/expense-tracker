using System.Runtime.Caching;

namespace Base.Caching.Cachers
{
	[Serializable]
	public class MemoryCacher : IMemoryCache
	{

		#region CTOR

		private ObjectCache _cache;

		public MemoryCacher()
		{
			_cache = MemoryCache.Default;
		}

		#endregion

		public void Set<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
		{
			// 60 secs expiration by default
			DateTimeOffset absoluteExpiration = absoluteExpireTime != null
				? DateTime.Now.Add(absoluteExpireTime.Value)
				: DateTime.Now.AddSeconds(60);

			var policy = new CacheItemPolicy
			{
				AbsoluteExpiration = absoluteExpiration
			};

			_cache.Add(key, data, policy);
		}

		public T Get<T>(string key)
		{
			T data = (T)_cache.Get(key);

			return data;
		}

		public bool Contains(string key)
		{
			bool contains = _cache.Contains(key);

			return contains;
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		public void Clear()
		{
			List<string> cacheKeys = MemoryCache.Default.Select(x => x.Key).ToList();

			foreach (string cacheKey in cacheKeys)
				Remove(cacheKey);
		}

	}
}
