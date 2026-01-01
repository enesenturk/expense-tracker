using Base.DataAccess.Repositories.Base.Concrete;
using Base.DataIO.Csv;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;

namespace ExpenseTracker.Infrastructure.Repositories.Modules.Category
{
	public class CsvSubCategoryRepository : CsvRepositoryBase<t_sub_category>, ISubCategoryRepository
	{
		public CsvSubCategoryRepository(string filePath, ICsvIO csvIO) : base(filePath, csvIO)
		{
		}
	}
}
