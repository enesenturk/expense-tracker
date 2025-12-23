using ExpenseTracker.Domain.Resources;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules;

namespace ExpenseTracker.MobileApp.Pages
{
	public partial class LayoutPage : ContentPage
	{

		public LayoutPage()
		{
			InitializeComponent();

			lblProductHeader.Text = uiMessage.EXPENSE_TRACKER;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await AnimationHelper.StartFadeBlinkAsync(lblProductHeader);
		}

		public void SetPage(ContentPage page)
		{
			RenderBody.Content = page.Content;
		}

		#region Navigations

		private void OnHomeClicked(object sender, EventArgs e)
		{
			SetPage(new HomePage());
		}

		private void OnExpensesClicked(object sender, EventArgs e)
		{
			SetPage(new ExpensesPage());
		}

		private void OnCategoriesClicked(object sender, EventArgs e)
		{
			SetPage(new CategoriesPage());
		}

		private void OnReportsClicked(object sender, EventArgs e)
		{
			SetPage(new ReportsPage());
		}

		private void OnPreferencesClicked(object sender, EventArgs e)
		{
			SetPage(new PreferencesPage());
		}

		private void OnAboutClicked(object sender, EventArgs e)
		{
			SetPage(new AboutPage());
		}

		#endregion

	}
}
