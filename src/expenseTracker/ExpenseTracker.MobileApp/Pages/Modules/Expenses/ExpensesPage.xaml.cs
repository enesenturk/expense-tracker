using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.DeleteExpenseCommand.Dtos;
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

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

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

			DateTime now = DateTime.Now;

			dpStartDate.Date = now.AddDays(-1 * now.Day + 1);
			dpEndDate.Date = now;

			btnSearch.Text = uiMessage.SEARCH;
		}

		#endregion

		#region Read

		public override void LoadDataAsync()
		{
			base.OnAppearing();

			Search();
		}

		private void OnSearchClicked(object sender, EventArgs e)
		{
			Search();
		}

		private async void Search()
		{
			GetList_Expense_QueryDto query = new GetList_Expense_QueryDto
			{
				Start = dpStartDate.Date,
				End = dpEndDate.Date
			};

			BaseResponseModel<GetList_Expense_ResponseDto> response = await ProxyCallerAsync<GetList_Expense_QueryDto, GetList_Expense_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Expense_SingleResponseModel> records = _mapper.Map<List<GetList_Expense_SingleResponseModel>>(response.Response.Records);

			_expenses = new ObservableCollection<GetList_Expense_SingleResponseModel>(records);

			expenesesCollection.ItemsSource = _expenses;
		}

		#endregion

		#region Delete

		private async void OnDeleteClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			if (button.CommandParameter is Guid categoryId)
			{
				bool isConfirmed = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Are_you_sure, uiMessage.YES, uiMessage.NO);

				if (!isConfirmed)
					return;

				Delete_Expense_CommandDto command = new Delete_Expense_CommandDto
				{
					Id = categoryId
				};

				BaseResponseModel<Unit> response = await ProxyCallerAsync<Delete_Expense_CommandDto, Unit>(command);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				var remove = _expenses.FirstOrDefault(c => c.Id == categoryId);

				if (remove != null)
					_expenses.Remove(remove);

				Refresh(ref _expenses, o => o.OrderByDescending(i => i.Date), expenesesCollection);
			}
		}

		#endregion

	}
}
