namespace ExpenseTracker.MobileApp
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();
		}

		private void OnHomeClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

		private void OnExpensesClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

		private void OnCategoriesClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

		private void OnReportsClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

		private void OnPreferencesClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

		private void OnAboutClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync("//MainPage");
		}

	}

}
