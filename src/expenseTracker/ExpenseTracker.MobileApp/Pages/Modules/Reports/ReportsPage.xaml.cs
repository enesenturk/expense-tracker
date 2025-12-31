using AutoMapper;
using Base.Dto;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Response;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses.Models.Response;
using MediatR;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Reports
{
	public partial class ReportsPage : BaseContentPage
	{

		#region CTOR

		private readonly Guid _isNecessaryGuid = Guid.NewGuid();
		private ObservableCollection<GetList_Category_SingleResponseModel> _categories;
		private ObservableCollection<JSonDto> _dateFilterTypes;

		public ReportsPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			lblReports.Text = uiMessage.REPORTS;
			lblReports.TextColor = ColorConstants.Purple;

			SwitchFilterType(DateFilterTypeModel.ThisWeek);

			btnSearch.Text = uiMessage.SEARCH;

			_dateFilterTypes = new ObservableCollection<JSonDto>(
				DropDownHelper.GetDropDownFromEnum<DateFilterTypeModel>(addSelectOption: false)
				);
			pickerDateType.ItemsSource = _dateFilterTypes;
			pickerDateType.ItemDisplayBinding = new Binding(nameof(JSonDto.Value));
			pickerDateType.SelectedIndex = 0;
		}

		#endregion

		#region Read

		public override async Task LoadDataAsync()
		{
			base.OnAppearing();

			GetList_Category_QueryDto query = new GetList_Category_QueryDto
			{
				Culture = SettingsHelper.GetCultureCode()
			};

			BaseResponseModel<GetList_Category_ResponseDto> response = await ProxyCallerAsync<GetList_Category_QueryDto, GetList_Category_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Category_SingleResponseModel> records = _mapper.Map<List<GetList_Category_SingleResponseModel>>(response.Response.Records);

			records.Insert(0, new GetList_Category_SingleResponseModel
			{
				Id = Guid.Empty,
				Name = uiMessage.ALL
			});
			records.Insert(1, new GetList_Category_SingleResponseModel
			{
				Id = _isNecessaryGuid,
				Name = uiMessage.NECESSARY_UNNECESSARY
			});

			_categories = new ObservableCollection<GetList_Category_SingleResponseModel>(records);

			pickerCategory.ItemsSource = _categories;
			pickerCategory.ItemDisplayBinding = new Binding(nameof(GetList_Category_SingleResponseModel.Name));
			pickerCategory.SelectedIndex = 0;

			Search();
		}

		private void OnSearchClicked(object sender, EventArgs e)
		{
			Search();
		}

		private async void Search()
		{
			GetList_Category_SingleResponseModel selectedCategory = pickerCategory.SelectedItem as GetList_Category_SingleResponseModel;

			bool isCategoryFiltered = selectedCategory != null && selectedCategory.Id != Guid.Empty && selectedCategory.Id != _isNecessaryGuid;

			GetList_Expense_QueryDto query = new GetList_Expense_QueryDto
			{
				CategoryId = isCategoryFiltered ? selectedCategory.Id : null,
				Start = dpStartDate.Date,
				End = dpEndDate.Date
			};

			BaseResponseModel<GetList_Expense_ResponseDto> response = await ProxyCallerAsync<GetList_Expense_QueryDto, GetList_Expense_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Expense_SingleResponseModel> records = _mapper.Map<List<GetList_Expense_SingleResponseModel>>(response.Response.Records);

			List<ChartEntry> chartEntries = isCategoryFiltered ?

				records
					.GroupBy(x => x.SubCategoryName)
					.Select((g, index) => new ChartEntry((float)g.Sum(x => x.Amount))
					{
						Label = g.Key,
						ValueLabel = $"{g.Sum(x => x.Amount).ToString()} {SettingsHelper.GetCurrency()}",
						Color = SKColor.Parse(ColorConstants.GetColorByIndex(index))
					})
					.ToList()

				:

				selectedCategory.Id == _isNecessaryGuid ?

					records
						.GroupBy(x => x.IsNecessary)
						.Select((g, index) => new ChartEntry((float)g.Sum(x => x.Amount))
						{
							Label = g.Key ? uiMessage.NECESSARY : uiMessage.UNNECESSARY,
							ValueLabel = $"{g.Sum(x => x.Amount).ToString()} {SettingsHelper.GetCurrency()}",
							Color = SKColor.Parse(ColorConstants.GetColorByIndex(index))
						})
						.ToList()

					:

					records
						.GroupBy(x => x.CategoryName)
						.Select((g, index) => new ChartEntry((float)g.Sum(x => x.Amount))
						{
							Label = g.Key,
							ValueLabel = $"{g.Sum(x => x.Amount).ToString()} {SettingsHelper.GetCurrency()}",
							Color = SKColor.Parse(ColorConstants.GetColorByIndex(index))
						})
						.ToList();

			chartView.Chart = new PieChart
			{
				Entries = chartEntries,
				LabelTextSize = 30,
				BackgroundColor = SKColor.Parse(ColorConstants.SoftGreyCode)
			};
		}

		private void OnDateTypeChanged(object sender, EventArgs e)
		{
			JSonDto selectedDateType = pickerDateType.SelectedItem as JSonDto;

			if (selectedDateType != null)
			{
				DateFilterTypeModel filterType = (DateFilterTypeModel)Convert.ToInt16(selectedDateType.Key);

				SwitchFilterType(filterType);
			}
		}

		private void SwitchFilterType(DateFilterTypeModel filterType)
		{
			DateTime today = DateTime.Today;

			switch (filterType)
			{
				case DateFilterTypeModel.ThisYear:
					{
						int monthStartDay = SettingsHelper.GetMonthStartDay();

						var filters = DatePeriodHelper.GetThisYear(monthStartDay);

						dpStartDate.Date = filters.FilterStart;
						dpEndDate.Date = filters.FilterEnd;

						break;
					}
				case DateFilterTypeModel.ThisMonth:
					{
						int monthStartDay = SettingsHelper.GetMonthStartDay();

						var filters = DatePeriodHelper.GetThisMonth(monthStartDay);

						dpStartDate.Date = filters.FilterStart;
						dpEndDate.Date = filters.FilterEnd;

						break;
					}
				default:
					{
						int firstDayOfWeek = SettingsHelper.GetFirstDayOfWeek();

						var filters = DatePeriodHelper.GetThisWeek(firstDayOfWeek);

						dpStartDate.Date = filters.FilterStart;
						dpEndDate.Date = filters.FilterEnd;

						break;
					}
			}
		}

		#endregion

	}
}