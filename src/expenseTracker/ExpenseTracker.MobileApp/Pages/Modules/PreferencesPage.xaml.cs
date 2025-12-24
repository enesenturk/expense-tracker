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

			lblPreferences.Text = uiMessage.PREFERENCES;
			lblPreferences.TextColor = ColorConstants.Purple;

			lblCurrency.Text = uiMessage.CURRENCY;
			lblLanguage.Text = uiMessage.LANGUAGE;
			lblFirstDayOfWeek.Text = uiMessage.FIRST_DAY_OF_WEEK;
			btnSave.Text = uiMessage.SAVE;
			btnSave.BackgroundColor = ColorConstants.Purple;

			pickerCurrency.ItemsSource = LocalizationHelper.GetSupportedCurrencies();
			pickerLanguage.ItemsSource = LocalizationHelper.GetSupportedLanguages();
			pickerFirstDayOfWeek.ItemsSource = _days;

			LoadPreferences();
		}


		void LoadPreferences()
		{
			string selectedCulture = Preferences.Get(LocalizationHelper.Culture, LocalizationHelper.Turkish);

			string cultureDisplay = selectedCulture == LocalizationHelper.TurkishCulture
				? LocalizationHelper.Turkish
				: LocalizationHelper.English;

			pickerLanguage.SelectedItem = cultureDisplay;
			pickerCurrency.SelectedItem = Preferences.Get(LocalizationHelper.Currency, LocalizationHelper.TurkishLira);

			pickerFirstDayOfWeek.SelectedItem = _days.First(
				x => x.Key == Preferences.Get(LocalizationHelper.FirstDayOfWeek, LocalizationHelper.DefaultFirstDayOfWeek).ToString()
				);
		}


		private async void OnSaveClicked(object sender, EventArgs e)
		{
			string selectedLanguage = pickerLanguage.SelectedItem.ToString();

			string cultureCode = selectedLanguage == LocalizationHelper.Turkish
				? LocalizationHelper.TurkishCulture
				: LocalizationHelper.EnglishCulture;

			Preferences.Set(LocalizationHelper.Currency, pickerCurrency.SelectedItem.ToString());
			Preferences.Set(LocalizationHelper.Culture, cultureCode);
			Preferences.Set(LocalizationHelper.FirstDayOfWeek, int.Parse(((JSonDto)pickerFirstDayOfWeek.SelectedItem).Key));

			CultureInfo culture = new CultureInfo(cultureCode);

			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

			await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.SUCCESSFULL, uiMessage.Preferences_Saved, uiMessage.OK);

			var layout = new LayoutPage();
			Microsoft.Maui.Controls.Application.Current.MainPage = layout;
			layout.SetPage(new HomePage());
		}

	}
}