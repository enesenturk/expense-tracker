using AutoMapper;
using ExpenseTracker.MobileApp.Base;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules
{
	public partial class AboutPage : BaseContentPage
	{
		public AboutPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();
		}
	}
}