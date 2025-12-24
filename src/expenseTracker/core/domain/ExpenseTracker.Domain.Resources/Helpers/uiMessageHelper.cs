using ExpenseTracker.Domain.Resources.Languages;
using System.Globalization;

namespace ExpenseTracker.Domain.Resources.Helpers
{
	public class uiMessageHelper
	{

		public static string GetUiMessage(string languageKey)
		{
			return uiMessage.ResourceManager.GetString(languageKey, CultureInfo.CurrentUICulture);
		}

	}
}
