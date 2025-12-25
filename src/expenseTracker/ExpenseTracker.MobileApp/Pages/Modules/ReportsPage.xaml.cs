using AutoMapper;
using ExpenseTracker.MobileApp.Base;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class ReportsPage : BaseContentPage
	{
		public ReportsPage(IMediator mediator, IMapper mapper, BaseMediatorCaller baseMediatorCaller)
			: base(mediator, mapper, baseMediatorCaller)
		{
			InitializeComponent();
		}
	}
}