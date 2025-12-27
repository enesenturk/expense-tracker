using AutoMapper;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Constants;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages.Modules.About
{
	public partial class AboutPage : BaseContentPage
	{
		public AboutPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			lblAbout.Text = uiMessage.ABOUT;
			lblAbout.TextColor = ColorConstants.Purple;

		}
	}
}