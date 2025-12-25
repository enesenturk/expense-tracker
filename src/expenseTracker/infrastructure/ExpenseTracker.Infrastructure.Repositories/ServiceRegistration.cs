using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;
using ExpenseTracker.Infrastructure.Repositories.Modules.Category;
using ExpenseTracker.Infrastructure.Repositories.Modules.Expense;
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

			services.AddScoped<IExpenseRepository>(sp =>
				new CsvExpenseRepository(Path.Combine(destinationPath, "expenses.csv"))
				);

		}
	}
}
