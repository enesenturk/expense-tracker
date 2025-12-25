using AutoMapper;
using ExpenseTracker.MobileApp.Base;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses
{
	public partial class ExpensesPage : BaseContentPage
	{
		public ExpensesPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();
		}
	}
}
