using Base.DataAccess.Repositories.Base.Concrete;
using Base.DataIO.Csv;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Infrastructure.Repositories.Modules.Expense
{
	public class CsvExpenseRepository : CsvRepositoryBase<t_expense>, IExpenseRepository
	{
		public CsvExpenseRepository(string filePath, ICsvIO csvIO) : base(filePath, csvIO)
		{
		}
	}
}
