namespace Base.PrimitiveTypeHelpers._DateTime.Converters
{
	public class DateTimeConverters
	{

		public static string ExtractDateFromDateTime(string dateTime)
		{
			return dateTime.Substring(0, 10);
		}

		public static string GetDifferenceAsFormattedTime(DateTime start, DateTime end)
		{
			TimeSpan difference = end.Subtract(start);

			return TimeSpanToFormattedTime(difference);
		}

		public static string SecondsToFormattedTime(decimal seconds)
		{
			TimeSpan timespan = TimeSpan.FromSeconds((double)seconds);

			return TimeSpanToFormattedTime(timespan);
		}

		public static string TimeSpanToFormattedTime(TimeSpan timespan)
		{
			return timespan.ToString(@"hh\:mm\:ss");
		}

	}
}
