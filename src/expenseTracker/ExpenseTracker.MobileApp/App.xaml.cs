using AutoMapper;
using Base.DataIO.Csv;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages;
using ExpenseTracker.MobileApp.Pages.Modules.Home;
using MediatR;
using System.Globalization;

namespace ExpenseTracker.MobileApp
{
	public partial class App : Microsoft.Maui.Controls.Application
	{

		private readonly ICsvExporter _csvExporter;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public App(IMediator mediator, IMapper mapper, ICsvExporter csvExporter)
		{
			InitializeComponent();

			_csvExporter = csvExporter;
			_mediator = mediator;
			_mapper = mapper;

			SetThreadCulture();

			var layout = new LayoutPage(_mediator, _mapper, _csvExporter);
			MainPage = layout;
			layout.SetPage(new HomePage(_mediator, _mapper, _csvExporter));
		}

		private void SetThreadCulture()
		{
			string cultureCode = SettingsHelper.GetCultureCode();

			CultureInfo culture = new CultureInfo(cultureCode);
			CultureInfo.DefaultThreadCurrentCulture = culture;
			CultureInfo.DefaultThreadCurrentUICulture = culture;
		}

	}

}