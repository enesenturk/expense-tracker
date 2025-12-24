using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Infrastructure.Repositories.Modules.Category;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure.Repositories
{
	public static class ServiceRegistration
	{
		public static void AddRepositoryServices(this IServiceCollection services, string destinationPath)
		{
			services.AddScoped<ICategoryRepository>(sp =>
				new CsvCategoryRepository(Path.Combine(destinationPath, "categories.csv"))
				);

		}
	}
}
