using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class HomePage : ContentPage
	{

		public HomePage()
		{
			InitializeComponent();

			btnAddExpense.Text = uiMessage.LOG_EXPENSE;

			AnimationHelper.StartBlinking(btnFrame, ColorConstants.Purple, ColorConstants.SoftPurple);
		}

		private void OnAddExpenseClicked(object sender, EventArgs e)
		{
			if (Microsoft.Maui.Controls.Application.Current.MainPage is LayoutPage layout)
			{
				layout.SetPage(new CreateExpensePage());
			}
		}

	}

}
