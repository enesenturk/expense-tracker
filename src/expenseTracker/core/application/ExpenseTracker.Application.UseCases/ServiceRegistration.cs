using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.BusinessRules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseTracker.Application.UseCases
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddUseCaseCommonServices(this IServiceCollection services)
		{
			var assembly = Assembly.GetExecutingAssembly();

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

			services.AddAutoMapper(cfg =>
			{
				cfg.ReplaceMemberName("_", "");

				cfg.AddMaps(assembly);
			});

			services.AddScoped<Create_Category_Command_BusinessRules>();

			return services;
		}
	}
}