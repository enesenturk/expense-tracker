using AutoMapper;
using ExpenseTracker.MobileApp.Base;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class ReportsPage : BaseContentPage
	{
		public ReportsPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();
		}
	}
}