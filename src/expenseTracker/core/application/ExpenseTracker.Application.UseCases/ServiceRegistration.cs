using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand.BusinessRules;
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
			services.AddScoped<Update_Category_Command_BusinessRules>();
			services.AddScoped<Create_SubCategory_Command_BusinessRules>();
			services.AddScoped<Update_SubCategory_Command_BusinessRules>();

			return services;
		}
	}
}