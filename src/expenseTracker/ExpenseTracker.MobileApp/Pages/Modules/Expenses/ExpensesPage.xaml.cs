using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses.Models.Response;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses
{
	public partial class ExpensesPage : BaseContentPage
	{

		#region CTOR

		private ObservableCollection<GetList_Expense_SingleResponseModel> _expenses;

		public ExpensesPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			lblExpenses.Text = uiMessage.EXPENSES;
			lblExpenses.TextColor = ColorConstants.Purple;

			hdrDate.Text = uiMessage.DATE;
			hdrDate.TextColor = ColorConstants.Purple;
			hdrSubCategory.Text = uiMessage.SUB_CATEGORY;
			hdrSubCategory.TextColor = ColorConstants.Purple;
			hdrAmount.Text = uiMessage.AMOUNT;
			hdrAmount.TextColor = ColorConstants.Purple;
			hdrIsNecessary.Text = uiMessage.NECESSARY;
			hdrIsNecessary.TextColor = ColorConstants.Purple;
		}

		#endregion

		#region Read

		public override async void LoadDataAsync()
		{
			base.OnAppearing();

			GetList_Expense_QueryDto query = new GetList_Expense_QueryDto
			{
			};

			BaseResponseModel<GetList_Expense_ResponseDto> response = await ProxyCallerAsync<GetList_Expense_QueryDto, GetList_Expense_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Expense_SingleResponseModel> records = _mapper.Map<List<GetList_Expense_SingleResponseModel>>(response.Response.Records);

			_expenses = new ObservableCollection<GetList_Expense_SingleResponseModel>(records);

			expenesesCollection.ItemsSource = _expenses;
		}

		#endregion

	}
}
