namespace Base.PrimitiveTypeHelpers._DateTime.Entensions
{
	public static class TimeSpanOperationExtensions
	{

		public static TimeSpan AddMinutesToHour(this TimeSpan timeSpan, int minutes)
		{
			DateTime temp = new DateTime(2015, 05, 26, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

			temp = temp.AddMinutes(minutes);

			return new TimeSpan(temp.Hour, temp.Minute, temp.Second);
		}

	}
}
