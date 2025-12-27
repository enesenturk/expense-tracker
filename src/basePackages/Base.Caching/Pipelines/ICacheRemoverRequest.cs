namespace Base.Caching.Pipelines
{
	public interface ICacheRemoverRequest
	{
		bool IsBypassCache
		{
			get
			{
				return false;
			}
		}

		List<string> CacheKeys { get; }

	}
}
