using AutoMapper;
using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages;
using ExpenseTracker.MobileApp.Pages.Modules.Home;
using MediatR;
using System.Globalization;

namespace ExpenseTracker.MobileApp
{
	public partial class App : Microsoft.Maui.Controls.Application
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public App(IMediator mediator, IMapper mapper)
		{
			InitializeComponent();

			_mediator = mediator;
			_mapper = mapper;

			SetDefaultLocalization();

			var layout = new LayoutPage(_mediator, _mapper);
			MainPage = layout;
			layout.SetPage(new HomePage(_mediator, _mapper));
		}

		private void SetDefaultLocalization()
		{
			PreferencesHelper.SetCurrency(LocalizationHelper.TurkishLira);
			PreferencesHelper.SetFirstDayOfWeek(LocalizationHelper.DefaultFirstDayOfWeek);
			PreferencesHelper.SetMonthStartDay(LocalizationHelper.DefaultMonthStartDay);

			string cultureCode = PreferencesHelper.GetCultureCode();

			CultureInfo culture = new CultureInfo(cultureCode);
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;
		}

	}

}