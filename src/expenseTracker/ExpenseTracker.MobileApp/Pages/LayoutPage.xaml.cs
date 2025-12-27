using AutoMapper;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.About;
using ExpenseTracker.MobileApp.Pages.Modules.Categories;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses;
using ExpenseTracker.MobileApp.Pages.Modules.Home;
using ExpenseTracker.MobileApp.Pages.Modules.Reports;
using ExpenseTracker.MobileApp.Pages.Modules.UserPreferences;
using MediatR;

namespace ExpenseTracker.MobileApp.Pages
{
	public partial class LayoutPage : BaseContentPage
	{

		public LayoutPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			lblProductHeader.Text = uiMessage.EXPENSE_TRACKER;

			gridLayout.BackgroundColor = ColorConstants.Purple;
			gridNavbar.BackgroundColor = ColorConstants.SoftPurple;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			AnimationHelper.StartFadeBlinkAsync(lblProductHeader);
		}

		public async void SetPage(BaseContentPage page)
		{
			RenderBody.Content = page.Content;

			await page.LoadDataAsync();
		}

		#region Navigations

		private void OnHomeClicked(object sender, EventArgs e)
		{
			SetPage(new HomePage(_mediator, _mapper));
		}

		private void OnExpensesClicked(object sender, EventArgs e)
		{
			SetPage(new ExpensesPage(_mediator, _mapper));
		}

		private void OnCategoriesClicked(object sender, EventArgs e)
		{
			SetPage(new CategoriesPage(_mediator, _mapper));
		}

		private void OnReportsClicked(object sender, EventArgs e)
		{
			SetPage(new ReportsPage(_mediator, _mapper));
		}

		private void OnPreferencesClicked(object sender, EventArgs e)
		{
			SetPage(new PreferencesPage(_mediator, _mapper));
		}

		private void OnAboutClicked(object sender, EventArgs e)
		{
			SetPage(new AboutPage(_mediator, _mapper));
		}

		#endregion

	}
}
