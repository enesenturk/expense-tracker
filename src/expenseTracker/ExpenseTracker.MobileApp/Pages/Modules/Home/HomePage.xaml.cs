using AutoMapper;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules.Home
{
	public partial class HomePage : BaseContentPage
	{

		public HomePage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			btnAddExpense.Text = uiMessage.LOG_EXPENSE;

			AnimationHelper.StartBlinking(btnFrame, ColorConstants.Purple, ColorConstants.SoftPurple);
		}

		private void OnAddExpenseClicked(object sender, EventArgs e)
		{
			if (Microsoft.Maui.Controls.Application.Current.MainPage is LayoutPage layout)
			{
				layout.SetPage(new CreateExpensePage(_mediator, _mapper));
			}
		}

	}

}
