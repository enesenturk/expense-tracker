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

			lblClosedSystemTitle.Text = uiMessage.All_safe_and_closed_system;
			lblClosedSystemDescription.Text = uiMessage.All_data_stored_locally;

			lblLocalDataTitle.Text = uiMessage.Your_data_is_yours;
			lblLocalDataDescription.Text = uiMessage.Your_expenses_categories_reports_local;

			lblCustomizationTitle.Text = uiMessage.Customizable_experience;
			lblCustomizationDescription.Text = uiMessage.Personalize_dates_categories_reports;

			lblReportingTitle.Text = uiMessage.Clear_and_simple_reporting;
			lblReportingDescription.Text = uiMessage.Analyze_expenses_with_charts;

			lblSimpleFastTitle.Text = uiMessage.Simple_and_fast;
			lblSimpleFastDescription.Text = uiMessage.Clean_and_distraction_free_design;

		}
	}
}