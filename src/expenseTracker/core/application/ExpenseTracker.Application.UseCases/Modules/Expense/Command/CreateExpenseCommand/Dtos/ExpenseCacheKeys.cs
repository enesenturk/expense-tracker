using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseThisMonthQuery.Dtos;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos
{
	public class ExpenseCacheKeys
	{
		public static List<string> TotalExpenseThisMonth(int monthStartDay)
		{
			return new List<string> {
				$"{nameof(Get_TotalExpenseThisMonth_QueryDto)}_{monthStartDay}"
			};
		}
	}
}
