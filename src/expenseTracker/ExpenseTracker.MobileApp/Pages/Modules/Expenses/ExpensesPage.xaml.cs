using AutoMapper;
using ExpenseTracker.MobileApp.Base;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses
{
	public partial class ExpensesPage : BaseContentPage
	{
		public ExpensesPage(IMediator mediator, IMapper mapper, BaseMediatorCaller baseMediatorCaller)
			: base(mediator, mapper, baseMediatorCaller)
		{
			InitializeComponent();
		}
	}
}
