namespace ExpenseTracker.Application.Utilities.Helpers
{
	public class DatePeriodHelper
	{

		public static (DateTime FilterStart, DateTime FilterEnd) GetThisWeek(int weekStartDay)
		{
			DateTime today = DateTime.Today;

			int todayDay = today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)today.DayOfWeek;
			int diff = todayDay - weekStartDay;
			if (diff < 0)
				diff += 7;

			DateTime weekStart = today.AddDays(-diff).Date;
			DateTime weekEnd = weekStart.AddDays(7);

			return (weekStart, weekEnd);
		}

		public static (DateTime FilterStart, DateTime FilterEnd) GetThisMonth(int monthStartDay)
		{
			DateTime today = DateTime.Today;

			int safeStartDay = Math.Min(
				monthStartDay,
				DateTime.DaysInMonth(today.Year, today.Month)
			);

			DateTime currentMonthStart = new DateTime(today.Year, today.Month, safeStartDay);

			if (today < currentMonthStart)
			{
				DateTime prevMonth = today.AddMonths(-1);

				safeStartDay = Math.Min(
					monthStartDay,
					DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month)
				);

				currentMonthStart = new DateTime(prevMonth.Year, prevMonth.Month, safeStartDay);
			}

			DateTime nextMonthStart = currentMonthStart.AddMonths(1);

			return (currentMonthStart, nextMonthStart);
		}

		public static (DateTime FilterStart, DateTime FilterEnd) GetThisYear(int monthStartDay)
		{
			DateTime today = DateTime.Today;

			int safeStartDay = Math.Min(
				monthStartDay,
				DateTime.DaysInMonth(today.Year, 1)
			);

			DateTime yearStart = new DateTime(today.Year, 1, safeStartDay);

			if (today < yearStart)
			{
				int prevYear = today.Year - 1;

				safeStartDay = Math.Min(
					monthStartDay,
					DateTime.DaysInMonth(prevYear, 1)
				);

				yearStart = new DateTime(prevYear, 1, safeStartDay);
			}

			DateTime nextYearStart = yearStart.AddYears(1);

			return (yearStart, nextYearStart);
		}

	}
}
