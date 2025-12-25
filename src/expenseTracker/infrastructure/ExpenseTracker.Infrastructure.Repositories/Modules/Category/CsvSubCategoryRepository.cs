using Base.DataAccess.Repositories.Base.Concrete;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;

namespace ExpenseTracker.Infrastructure.Repositories.Modules.Category
{
	public class CsvSubCategoryRepository : CsvRepositoryBase<t_sub_category>, ISubCategoryRepository
	{
		public CsvSubCategoryRepository(string filePath) : base(filePath)
		{
		}
	}
}
