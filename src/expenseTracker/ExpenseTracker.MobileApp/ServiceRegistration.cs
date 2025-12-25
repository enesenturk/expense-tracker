using ExpenseTracker.MobileApp.Base;
using System.Reflection;

namespace ExpenseTracker.MobileApp
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddMobileAppServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");

				cfg.AddMaps(assembly);
			});

			services.AddScoped<BaseMediatorCaller>();

			return services;
		}
	}
}
