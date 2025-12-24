using Base.Dto;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Domain.Enums;
using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Constants;
using System.Globalization;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class PreferencesPage : ContentPage
	{

		private readonly List<JSonDto> _days = DropDownHelper.GetDropDownFromEnum<Days>(addSelectOption: false);

		public PreferencesPage()
		{
			InitializeComponent();

			lblPreferences.Text = uiMessage.CATEGORIES;
			lblPreferences.TextColor = ColorConstants.Purple;

			lblCurrency.Text = uiMessage.CURRENCY;
			lblLanguage.Text = uiMessage.LANGUAGE;
			lblFirstDayOfWeek.Text = uiMessage.FIRST_DAY_OF_WEEK;
			btnSave.Text = uiMessage.SAVE;

			pickerCurrency.ItemsSource = LocalizationHelper.GetSupportedCurrencies();
			pickerLanguage.ItemsSource = LocalizationHelper.GetSupportedLanguages();
			pickerFirstDayOfWeek.ItemsSource = _days;

			LoadPreferences();
		}


		void LoadPreferences()
		{
			bool isTurkish = CultureInfo.CurrentUICulture.Name.StartsWith("tr");

			if (!Preferences.ContainsKey(LocalizationHelper.Language))
			{
				pickerLanguage.SelectedItem = isTurkish ? LocalizationHelper.Turkish : LocalizationHelper.English;
			}
			else
			{
				pickerLanguage.SelectedItem = Preferences.Get(LocalizationHelper.Language, LocalizationHelper.Turkish);
			}

			if (!Preferences.ContainsKey(LocalizationHelper.Currency))
			{
				pickerCurrency.SelectedItem = isTurkish ? LocalizationHelper.TurkishLira : LocalizationHelper.USD;
			}
			else
			{
				pickerCurrency.SelectedItem = Preferences.Get(LocalizationHelper.Currency, LocalizationHelper.TurkishLira);
			}

			pickerFirstDayOfWeek.SelectedItem = _days.First(
				x => x.Key == Preferences.Get(LocalizationHelper.FirstDayOfWeek, LocalizationHelper.DefaultFirstDayOfWeek).ToString()
				);
		}


		private async void OnSaveClicked(object sender, EventArgs e)
		{
			string selectedLanguage = pickerLanguage.SelectedItem.ToString();

			Preferences.Set(LocalizationHelper.Currency, pickerCurrency.SelectedItem.ToString());
			Preferences.Set(LocalizationHelper.Language, selectedLanguage);
			Preferences.Set(LocalizationHelper.FirstDayOfWeek, (int)pickerFirstDayOfWeek.SelectedItem);

			string cultureCode = selectedLanguage == LocalizationHelper.Turkish
				? "tr-TR"
				: "en-US";

			CultureInfo.CurrentCulture = new CultureInfo(cultureCode);
			CultureInfo.CurrentUICulture = new CultureInfo(cultureCode);

			await DisplayAlert("Saved", "Preferences saved successfully.", "OK");
		}

	}
}