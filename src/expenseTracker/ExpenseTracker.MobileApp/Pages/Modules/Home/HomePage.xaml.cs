using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseThisMonthQuery.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules.Home
{
	public partial class HomePage : BaseContentPage
	{

		#region CTOR

		public HomePage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			btnAddExpense.Text = uiMessage.LOG_EXPENSE;

			lblMonthlyTitle.TextColor = ColorConstants.MiddlePurple;
			lblMonthlyTitle.Text = uiMessage.MONTHLY_EXPENSES;

			lblMonthlyAmount.TextColor = ColorConstants.Purple;

			AnimationHelper.StartBlinking(btnFrame, ColorConstants.Purple, ColorConstants.SoftPurple);
		}

		#endregion

		#region Create

		private void OnAddExpenseClicked(object sender, EventArgs e)
		{
			if (Microsoft.Maui.Controls.Application.Current.MainPage is LayoutPage layout)
			{
				layout.SetPage(new CreateExpensePage(_mediator, _mapper));
			}
		}

		#endregion

		#region Read

		public override async Task LoadDataAsync()
		{
			base.OnAppearing();

			Get_TotalExpenseThisMonth_QueryDto query = new Get_TotalExpenseThisMonth_QueryDto
			{
				MonthStartDay = SettingsHelper.GetMonthStartDay()
			};

			BaseResponseModel<Get_TotalExpenseThisMonth_ResponseDto> response = await ProxyCallerAsync<Get_TotalExpenseThisMonth_QueryDto, Get_TotalExpenseThisMonth_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			lblMonthlyAmount.Text = $"{response.Response.TotalAmount} {SettingsHelper.GetCurrency()}";
		}

		#endregion

	}

}
