using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Constants;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses
{

	public partial class CreateExpensePage : ContentPage
	{
		public CreateExpensePage()
		{
			InitializeComponent();

			datePicker.Date = DateTime.Now;
			lblPage.Text = uiMessage.ADD_EXPENSE;
			lblPage.TextColor = ColorConstants.Purple;

			lblDate.Text = uiMessage.DATE;
			lblCategory.Text = uiMessage.CATEGORY;
			pickerCategory.Title = uiMessage.SELECT;
			lblSubCategory.Text = uiMessage.SUB_CATEGORY;
			pickerSubCategory.Title = uiMessage.SELECT;
			lblAmount.Text = uiMessage.AMOUNT;
			lblExpenseType.Text = uiMessage.STATUS;
			btnSave.Text = uiMessage.SAVE;
			btnSave.BackgroundColor = ColorConstants.Purple;
			rbNecessary.Content = uiMessage.NECESSARY;
			rbUnnecessary.Content = uiMessage.UNNECESSARY;
		}

		private void OnAddSaveClicked(object sender, EventArgs e)
		{
			List<string> errors = new List<string>();

			DateTime date = datePicker.Date;
			string category = pickerCategory.SelectedItem as string;
			string subCategory = pickerSubCategory.SelectedItem as string;
			string amountText = amountEntry.Text;
			bool amountValid = decimal.TryParse(amountText, out decimal amount);
			bool isNecessary = rbUnnecessary.IsChecked;

			if (!amountValid || amount <= 0)
			{
				Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Uyarı", "Lütfen amount giriniz!", "Tamam");
				return;
			}

			string status = isNecessary ? "Gerekli" : "Gereksiz";
			string categoryText = string.IsNullOrEmpty(category) ? "-" : category;

			DisplayAlert("Başarılı",
				$"Tarih: {date.ToShortDateString()}\nKategori: {categoryText}\nAlt Kategori: {subCategory}\nTutar: {amount:N2}\nDurum: {status}",
				"Tamam");
		}

	}
}