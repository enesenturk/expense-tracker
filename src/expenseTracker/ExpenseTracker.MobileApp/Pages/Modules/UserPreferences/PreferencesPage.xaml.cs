using AutoMapper;
using Base.Dto;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Home;
using MediatR;
using System.Globalization;

namespace ExpenseTracker.MobileApp.Pages.Modules.UserPreferences
{
	public partial class PreferencesPage : BaseContentPage
	{

		#region CTOR

		private readonly List<JSonDto> _days = DropDownHelper.GetDropDownFromEnum<DayOfWeek>(addSelectOption: false);
		private readonly List<int> _dayIndexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };

		public PreferencesPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			lblPreferences.Text = uiMessage.PREFERENCES;
			lblPreferences.TextColor = ColorConstants.Purple;

			lblCurrency.Text = uiMessage.CURRENCY;
			lblLanguage.Text = uiMessage.LANGUAGE;
			lblMonthStartDay.Text = uiMessage.MONTH_START_DAY;
			lblFirstDayOfWeek.Text = uiMessage.FIRST_DAY_OF_WEEK;
			btnSave.Text = uiMessage.SAVE;
			btnSave.BackgroundColor = ColorConstants.Purple;

			pickerCurrency.ItemsSource = LocalizationHelper.GetSupportedCurrencies();
			pickerLanguage.ItemsSource = LocalizationHelper.GetSupportedLanguages();
			pickerMonthStartDay.ItemsSource = _dayIndexes;
			pickerFirstDayOfWeek.ItemsSource = _days;

			LoadPreferences();
		}

		void LoadPreferences()
		{
			string cultureDisplayName = PreferencesHelper.GetCultureDisplayName();

			pickerLanguage.SelectedItem = cultureDisplayName;
			pickerCurrency.SelectedItem = PreferencesHelper.GetCurrency();

			pickerMonthStartDay.SelectedItem = _dayIndexes.First(
				x => x == PreferencesHelper.GetMonthStartDay()
				);
			pickerFirstDayOfWeek.SelectedItem = _days.First(
				x => x.Key == PreferencesHelper.GetFirstDayOfWeek().ToString()
				);
		}

		#endregion

		#region Update

		private async void OnSaveClicked(object sender, EventArgs e)
		{
			string newCultureDisplayName = pickerLanguage.SelectedItem.ToString();
			string newCultureCode = PreferencesHelper.GetCultureCode(newCultureDisplayName);

			PreferencesHelper.SetCultureCode(newCultureCode);
			PreferencesHelper.SetCurrency(pickerCurrency.SelectedItem.ToString());
			PreferencesHelper.SetFirstDayOfWeek(int.Parse(((JSonDto)pickerFirstDayOfWeek.SelectedItem).Key));
			PreferencesHelper.SetMonthStartDay(int.Parse(pickerMonthStartDay.SelectedItem.ToString()));

			CultureInfo culture = new CultureInfo(newCultureCode);

			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

			await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.SUCCESSFUL, uiMessage.Preferences_saved, uiMessage.OK);

			var layout = new LayoutPage(_mediator, _mapper);
			Microsoft.Maui.Controls.Application.Current.MainPage = layout;
			layout.SetPage(new HomePage(_mediator, _mapper));
		}

		#endregion

	}
}