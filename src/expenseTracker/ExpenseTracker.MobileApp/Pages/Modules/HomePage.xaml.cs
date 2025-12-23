using ExpenseTracker.Domain.Resources;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class HomePage : ContentPage
	{

		public HomePage()
		{
			InitializeComponent();

			btnLogExpense.Text = uiMessage.Log_Expense;
		}

		private async void OnAddExpenseClicked(object sender, EventArgs e)
		{
			await DisplayAlert("Harcama", "Harcama Gir butonuna tıklandı!", "Tamam");
		}

	}

}
