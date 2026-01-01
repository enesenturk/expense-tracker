using Base.DataIO.Csv;
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

			services.AddSingleton<ISubCategoryRepository>(sp =>
			{
				ICsvIO csvIO = sp.GetRequiredService<ICsvIO>();

				return new CsvSubCategoryRepository(Path.Combine(destinationPath, "subcategories.csv"), csvIO);
			});

			services.AddSingleton<ICategoryRepository>(sp =>
			{
				ICsvIO csvIO = sp.GetRequiredService<ICsvIO>();

				return new CsvCategoryRepository(Path.Combine(destinationPath, "categories.csv"), csvIO);
			});

			services.AddSingleton<IExpenseRepository>(sp =>
			{
				ICsvIO csvIO = sp.GetRequiredService<ICsvIO>();

				return new CsvExpenseRepository(Path.Combine(destinationPath, "expenses.csv"), csvIO);
			});

		}
	}
}
