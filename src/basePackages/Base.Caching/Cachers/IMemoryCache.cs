namespace Base.Caching.Cachers
{
	public interface IMemoryCache
	{
		void Set<T>(string key, T data, TimeSpan? absoluteExpireTime = null);
		T Get<T>(string key);
		void Remove(string key);
		bool Contains(string key);
		void Clear();
	}
}
