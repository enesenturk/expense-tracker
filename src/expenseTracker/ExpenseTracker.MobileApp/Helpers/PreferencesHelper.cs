using ExpenseTracker.Domain.Resources.Helpers;

namespace ExpenseTracker.MobileApp.Helpers
{
	public class PreferencesHelper
	{

		public static string GetCulture()
		{
			return Preferences.Get(LocalizationHelper.Culture, LocalizationHelper.Turkish);
		}

		internal static int GetFirstDayOfWeek()
		{
			return Preferences.Get(LocalizationHelper.FirstDayOfWeek, LocalizationHelper.DefaultFirstDayOfWeek);
		}

	}
}
