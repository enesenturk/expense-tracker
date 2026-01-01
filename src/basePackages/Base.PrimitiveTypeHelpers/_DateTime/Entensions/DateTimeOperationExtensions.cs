namespace Base.PrimitiveTypeHelpers._DateTime.Entensions
{
	public static class DateTimeOperationExtensions
	{

		public static DateTime SubstractMinutes(this DateTime dateTime, int minutes)
		{
			return dateTime.AddMinutes(-1 * minutes);
		}

		public static DateTime RemoveMiliseconds(this DateTime dateTime)
		{
			return dateTime.AddMilliseconds(-1 * dateTime.Millisecond);
		}

		public static DateTime ToUniversalTimeZone(this DateTime dateTime)
		{
			if (dateTime.TimeOfDay == new TimeSpan(0, 0, 0))
				return dateTime;

			return dateTime.ToUniversalTime();
		}

		public static DateTime ToLocalTimeZone(this DateTime dateTime, int timeDifferenceAsMinutes)
		{
			DateTime localTimeZone = DateTime.Now.ToUniversalTimeZone().AddMinutes(timeDifferenceAsMinutes);

			return localTimeZone;
		}

	}
}
