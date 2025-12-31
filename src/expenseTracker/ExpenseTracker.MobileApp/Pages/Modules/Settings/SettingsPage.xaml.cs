using AutoMapper;
using Base.DataIO.Csv;
using Base.Dto;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Home;
using ExpenseTracker.MobileApp.Pages.Modules.Settings.Models.Response;
using MediatR;
using System.Globalization;

namespace ExpenseTracker.MobileApp.Pages.Modules.Settings
{
	public partial class SettingsPage : BaseContentPage
	{

		#region CTOR

		private readonly List<JSonDto> _days = DropDownHelper.GetDropDownFromEnum<DayOfWeek>(addSelectOption: false);
		private readonly List<int> _dayIndexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28 };

		private readonly ICsvExporter _csvExporter;

		public SettingsPage(IMediator mediator, IMapper mapper, ICsvExporter csvExporter)
			: base(mediator, mapper)
		{
			InitializeComponent();

			_csvExporter = csvExporter;

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			lblSettings.Text = uiMessage.SETTINGS;
			lblSettings.TextColor = ColorConstants.Purple;

			lblCurrency.Text = uiMessage.CURRENCY;
			lblLanguage.Text = uiMessage.LANGUAGE;
			lblMonthStartDay.Text = uiMessage.MONTH_START_DAY;
			lblFirstDayOfWeek.Text = uiMessage.FIRST_DAY_OF_WEEK;
			btnBackupInfo.TextColor = ColorConstants.Purple;
			frmSaveForm.BorderColor = ColorConstants.Purple;
			frmSaveForm.BackgroundColor = ColorConstants.SoftGrey;

			frmBackup.BackgroundColor = ColorConstants.SoftGrey;
			frmBackup.BorderColor = ColorConstants.Purple;
			frmBackupInfo.BackgroundColor = ColorConstants.SoftGrey;
			frmBackupInfo.BorderColor = ColorConstants.Purple;
			lblExportCsv.Text = uiMessage.EXPORT_DATA;
			lblExportCsv.TextColor = ColorConstants.Purple;
			lblImportCsv.Text = uiMessage.IMPORT_DATA;
			lblImportCsv.TextColor = ColorConstants.Purple;

			btnSave.Text = uiMessage.SAVE;
			btnSave.BackgroundColor = ColorConstants.Purple;

			pickerCurrency.ItemsSource = LocalizationHelper.GetSupportedCurrencies();
			pickerLanguage.ItemsSource = LocalizationHelper.GetSupportedLanguages();
			pickerMonthStartDay.ItemsSource = _dayIndexes;
			pickerFirstDayOfWeek.ItemsSource = _days;

			LoadSettings();
		}

		void LoadSettings()
		{
			string cultureDisplayName = SettingsHelper.GetCultureDisplayName();

			pickerLanguage.SelectedItem = cultureDisplayName;
			pickerCurrency.SelectedItem = SettingsHelper.GetCurrency();

			pickerMonthStartDay.SelectedItem = _dayIndexes.First(
				x => x == SettingsHelper.GetMonthStartDay()
				);
			pickerFirstDayOfWeek.SelectedItem = _days.First(
				x => x.Key == SettingsHelper.GetFirstDayOfWeek().ToString()
				);
		}

		#endregion

		#region Read

		private async void OnBackupInfoClicked(object sender, EventArgs e)
		{
			await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(
				uiMessage.Data_Backup_Restore,
				uiMessage.Data_Backup_Restore_Description,
				uiMessage.OK
			);
		}

		private async void OnExportCsvTapped(object sender, EventArgs e)
		{
			try
			{
				byte[] categories = await GetCategories();
				byte[] subCategories = await GetSubCategories();
				byte[] expenses = await GetExpenses();

				if (categories != null && subCategories != null && expenses != null)
				{
					string appDataDirectory = FileSystem.AppDataDirectory;

					string categoriesPath = Path.Combine(appDataDirectory, "export_categories.csv");
					string subCategoriesPath = Path.Combine(appDataDirectory, "export_subcategories.csv");
					string expensesPath = Path.Combine(appDataDirectory, "export_expenses.csv");

					await File.WriteAllBytesAsync(categoriesPath, categories);
					await File.WriteAllBytesAsync(subCategoriesPath, subCategories);
					await File.WriteAllBytesAsync(expensesPath, expenses);

					List<ShareFile> shareFiles = new List<ShareFile>
				{
					new ShareFile(categoriesPath),
					new ShareFile(subCategoriesPath),
					new ShareFile(expensesPath)
				};

					await Share.RequestAsync(new ShareMultipleFilesRequest
					{
						Title = "data",
						Files = shareFiles
					});
				}
			}
			catch
			{
				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.ERROR, uiMessage.Error_occurred, uiMessage.OK);
			}
		}

		private async void OnImportCsvTapped(object sender, EventArgs e)
		{

		}

		#region Behind the Scenes

		private async Task<byte[]> GetCategories()
		{
			GetList_CategoryTable_QueryDto query = new GetList_CategoryTable_QueryDto
			{
				Culture = SettingsHelper.GetCultureCode(),
			};

			BaseResponseModel<GetList_CategoryTable_ResponseDto> response = await ProxyCallerAsync<GetList_CategoryTable_QueryDto, GetList_CategoryTable_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return null;

			List<GetList_CategoryTable_SingleResponseModel> records = _mapper.Map<List<GetList_CategoryTable_SingleResponseModel>>(response.Response.Records);

			byte[] csv = _csvExporter.CreateCvs(records, isAddHeader: false);

			return csv;
		}

		private async Task<byte[]> GetSubCategories()
		{
			GetList_SubCategoryTable_QueryDto query = new GetList_SubCategoryTable_QueryDto();

			BaseResponseModel<GetList_SubCategoryTable_ResponseDto> response = await ProxyCallerAsync<GetList_SubCategoryTable_QueryDto, GetList_SubCategoryTable_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return null;

			List<GetList_SubCategoryTable_SingleResponseModel> records = _mapper.Map<List<GetList_SubCategoryTable_SingleResponseModel>>(response.Response.Records);

			byte[] csv = _csvExporter.CreateCvs(records, isAddHeader: false);

			return csv;
		}

		private async Task<byte[]> GetExpenses()
		{
			GetList_ExpenseTable_QueryDto query = new GetList_ExpenseTable_QueryDto();

			BaseResponseModel<GetList_ExpenseTable_ResponseDto> response = await ProxyCallerAsync<GetList_ExpenseTable_QueryDto, GetList_ExpenseTable_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return null;

			List<GetList_ExpenseTable_SingleResponseModel> records = _mapper.Map<List<GetList_ExpenseTable_SingleResponseModel>>(response.Response.Records);

			byte[] csv = _csvExporter.CreateCvs(records, isAddHeader: false);

			return csv;
		}

		#endregion

		#endregion

		#region Update

		private async void OnSaveClicked(object sender, EventArgs e)
		{
			string newCultureDisplayName = pickerLanguage.SelectedItem.ToString();
			string newCultureCode = SettingsHelper.GetCultureCode(newCultureDisplayName);

			SettingsHelper.SetCultureCode(newCultureCode);
			SettingsHelper.SetCurrency(pickerCurrency.SelectedItem.ToString());
			SettingsHelper.SetFirstDayOfWeek(int.Parse(((JSonDto)pickerFirstDayOfWeek.SelectedItem).Key));
			SettingsHelper.SetMonthStartDay(int.Parse(pickerMonthStartDay.SelectedItem.ToString()));

			CultureInfo culture = new CultureInfo(newCultureCode);

			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;

			await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.SUCCESSFUL, uiMessage.Settings_saved, uiMessage.OK);

			var layout = new LayoutPage(_mediator, _mapper, _csvExporter);
			Microsoft.Maui.Controls.Application.Current.MainPage = layout;
			layout.SetPage(new HomePage(_mediator, _mapper, _csvExporter));
		}

		#endregion

	}
}