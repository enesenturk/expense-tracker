namespace Base.Caching.Pipelines
{
	public interface ICachableRequest
	{

		string CacheKey
		{
			get
			{
				return null;
			}
		}

		string CacheKeyParameters
		{
			get
			{
				return "";
			}
		}

		bool IsBypassCache
		{
			get
			{
				return false;
			}
		}

		TimeSpan CacheExpiration
		{
			get
			{
				return TimeSpan.FromHours(1);
			}
		}

	}
}
