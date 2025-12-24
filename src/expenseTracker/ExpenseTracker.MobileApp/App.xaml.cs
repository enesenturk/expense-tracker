using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.MobileApp.Pages;
using ExpenseTracker.MobileApp.Pages.Modules;
using System.Globalization;

namespace ExpenseTracker.MobileApp
{
	public partial class App : Microsoft.Maui.Controls.Application
	{
		public App()
		{
			InitializeComponent();

			string cultureCode = Preferences.Get(LocalizationHelper.Culture, LocalizationHelper.TurkishCulture);

			CultureInfo culture = new CultureInfo(cultureCode);

			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

			var layout = new LayoutPage();

			MainPage = layout;

			layout.SetPage(new HomePage());
		}
	}
}