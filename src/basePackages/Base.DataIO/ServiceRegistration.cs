using Base.DataIO.Csv;
using Microsoft.Extensions.DependencyInjection;

namespace Base.DataIO
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddDataIOServices(this IServiceCollection services)
		{
			services.AddScoped<ICsvExporter, CsvExporter>();

			return services;
		}
	}
}
