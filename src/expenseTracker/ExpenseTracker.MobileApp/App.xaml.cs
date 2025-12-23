using ExpenseTracker.MobileApp.Pages;
using ExpenseTracker.MobileApp.Pages.Modules;

namespace ExpenseTracker.MobileApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var layout = new LayoutPage();

			layout.SetPage(new HomePage());

			MainPage = layout;
		}
	}
}