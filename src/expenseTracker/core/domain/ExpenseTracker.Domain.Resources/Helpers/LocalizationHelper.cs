namespace ExpenseTracker.Domain.Resources.Helpers
{
	public class LocalizationHelper
	{

		public static List<string> GetSupportedCurrencies()
		{
			return new List<string>
			{
				TurkishLira,
				USD
			};
		}

		public static List<string> GetSupportedLanguages()
		{
			return new List<string>
			{
				English,
				Turkish
			};
		}

		public static string Culture => "Culture";
		public static string English => "English";
		public static string EnglishCulture => "en-US";
		public static string Turkish => "Türkçe";
		public static string TurkishCulture => "tr-TR";

		public static string Currency => "Currency";
		public static string TurkishLira => "TRY (₺)";
		public static string USD => "USD ($)";

		public static string FirstDayOfWeek => "FirstDayOfWeek";
		public static int DefaultFirstDayOfWeek => (int)DayOfWeek.Monday;

	}
}