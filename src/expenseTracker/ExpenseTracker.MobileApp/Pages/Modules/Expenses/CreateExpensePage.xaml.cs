using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Response;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses
{
	public partial class CreateExpensePage : BaseContentPage
	{

		#region CTOR

		private ObservableCollection<GetList_Category_SingleResponseModel> _categories;

		public CreateExpensePage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

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

		#endregion

		#region Create

		private async void OnAddSaveClicked(object sender, EventArgs e)
		{
			List<string> errors = new List<string>();

			DateTime date = datePicker.Date;
			GetList_Category_SingleResponseModel category = pickerCategory.SelectedItem as GetList_Category_SingleResponseModel;
			GetList_SubCategory_SingleResponseModel subCategory = pickerSubCategory.SelectedItem as GetList_SubCategory_SingleResponseModel;
			bool amountValid = decimal.TryParse(amountEntry.Text, out decimal amount);
			bool isNecessary = rbNecessary.IsChecked;

			if (!amountValid || amount <= 0)
				errors.Add(uiMessage.Please_enter_valid_amount);

			if (category is null || category.Id == Guid.Empty)
				errors.Add(uiMessage.Please_select_category);

			if (subCategory is null || subCategory.Id == Guid.Empty)
				errors.Add(uiMessage.Please_select_sub_category);

			if (errors.Count > 0)
			{
				string errorMessage = string.Join('\n', errors);

				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, errorMessage, uiMessage.OK);
			}
			else
			{
				Create_Expense_CommandDto command = new Create_Expense_CommandDto
				{
					Date = date,
					CategoryId = category.Id,
					SubCategoryId = subCategory.Id,
					Amount = amount,
					IsNecessary = isNecessary
				};

				BaseResponseModel<Unit> response = await ProxyCallerAsync<Create_Expense_CommandDto, Unit>(command);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.SUCCESSFUL, uiMessage.Successfully_added, uiMessage.OK);

				var layout = new LayoutPage(_mediator, _mapper);
				Microsoft.Maui.Controls.Application.Current.MainPage = layout;
				layout.SetPage(new HomePage(_mediator, _mapper));
			}
		}

		#endregion

		#region Read

		public override async void LoadDataAsync()
		{
			base.OnAppearing();

			GetList_Category_QueryDto query = new GetList_Category_QueryDto
			{
				Culture = PreferencesHelper.GetCulture()
			};

			BaseResponseModel<GetList_Category_ResponseDto> response = await ProxyCallerAsync<GetList_Category_QueryDto, GetList_Category_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Category_SingleResponseModel> records = _mapper.Map<List<GetList_Category_SingleResponseModel>>(response.Response.Records);

			_categories = new ObservableCollection<GetList_Category_SingleResponseModel>(records);

			pickerCategory.ItemsSource = _categories;
			pickerCategory.ItemDisplayBinding = new Binding(nameof(GetList_Category_SingleResponseModel.Name));
		}

		private async void OnCategoryChanged(object sender, EventArgs e)
		{
			GetList_Category_SingleResponseModel selectedCategory = pickerCategory.SelectedItem as GetList_Category_SingleResponseModel;

			if (selectedCategory != null)
			{
				GetList_SubCategory_QueryDto query = new GetList_SubCategory_QueryDto
				{
					CategoryId = selectedCategory.Id
				};

				BaseResponseModel<GetList_SubCategory_ResponseDto> response = await ProxyCallerAsync<GetList_SubCategory_QueryDto, GetList_SubCategory_ResponseDto>(query);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				List<GetList_SubCategory_SingleResponseModel> records = _mapper.Map<List<GetList_SubCategory_SingleResponseModel>>(response.Response.Records);

				pickerSubCategory.ItemsSource = records;
				pickerSubCategory.ItemDisplayBinding = new Binding(nameof(GetList_SubCategory_SingleResponseModel.Name));
				pickerSubCategory.IsEnabled = true;
			}
		}

		#endregion

	}
}