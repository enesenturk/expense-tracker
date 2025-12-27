using ExpenseTracker.Domain.Resources.Helpers;

namespace ExpenseTracker.MobileApp.Helpers
{
	public class PreferencesHelper
	{

		#region Culture

		public static string GetCultureCode()
		{
			return Preferences.Get(LocalizationHelper.Culture, LocalizationHelper.TurkishCode);
		}

		internal static string GetCultureCode(string cultureDisplayName)
		{
			if (cultureDisplayName == LocalizationHelper.English)
			{
				return LocalizationHelper.EnglishCode;
			}
			else
			{
				return LocalizationHelper.TurkishCode;
			}
		}

		internal static string GetCultureDisplayName()
		{
			string cultureCode = GetCultureCode();

			if (cultureCode == LocalizationHelper.EnglishCode)
			{
				return LocalizationHelper.English;
			}
			else
			{
				return LocalizationHelper.Turkish;
			}
		}

		public static void SetCultureCode(string cultureCode)
		{
			Preferences.Set(LocalizationHelper.Culture, cultureCode);
		}

		#endregion

		#region Currency

		public static string GetCurrency()
		{
			return Preferences.Get(LocalizationHelper.Currency, LocalizationHelper.TurkishLira);
		}

		public static void SetCurrency(string currency)
		{
			Preferences.Set(LocalizationHelper.Currency, currency);
		}

		#endregion

		#region FirstDayOfWeek

		internal static int GetFirstDayOfWeek()
		{
			return Preferences.Get(LocalizationHelper.FirstDayOfWeek, LocalizationHelper.DefaultFirstDayOfWeek);
		}

		internal static void SetFirstDayOfWeek(int firstDayOfWeek)
		{
			Preferences.Set(LocalizationHelper.FirstDayOfWeek, firstDayOfWeek);
		}

		#endregion

		#region MonthStartDay

		internal static int GetMonthStartDay()
		{
			return Preferences.Get(LocalizationHelper.MonthStartDay, LocalizationHelper.DefaultMonthStartDay);
		}

		internal static void SetMonthStartDay(int startDay)
		{
			Preferences.Set(LocalizationHelper.MonthStartDay, startDay);
		}

		#endregion

	}
}
