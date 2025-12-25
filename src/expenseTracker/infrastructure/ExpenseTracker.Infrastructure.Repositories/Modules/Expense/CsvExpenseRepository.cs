using Base.DataAccess.Repositories.Base.Concrete;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Infrastructure.Repositories.Modules.Expense
{
	public class CsvExpenseRepository : CsvRepositoryBase<t_expense>, IExpenseRepository
	{
		public CsvExpenseRepository(string filePath) : base(filePath)
		{
		}
	}
}
