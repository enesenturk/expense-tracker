using Base.DataAccess.Repositories.Base.Concrete;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;

namespace ExpenseTracker.Infrastructure.Repositories.Modules.Category
{
	public class CsvCategoryRepository : CsvRepositoryBase<t_category>, ICategoryRepository
	{
		public CsvCategoryRepository(string filePath) : base(filePath)
		{
		}
	}
}
