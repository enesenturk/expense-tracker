using Base.Caching.Pipelines;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.DeleteExpenseCommand.Dtos
{
	public class Delete_Expense_CommandDto : IRequest<Unit>, ICacheRemoverRequest
	{
		public Guid Id { get; set; }
		public int MonthStartDay { get; set; }

		public List<string> CacheKeys => ExpenseCacheKeys.TotalExpenseThisMonth(MonthStartDay);
	}
}
