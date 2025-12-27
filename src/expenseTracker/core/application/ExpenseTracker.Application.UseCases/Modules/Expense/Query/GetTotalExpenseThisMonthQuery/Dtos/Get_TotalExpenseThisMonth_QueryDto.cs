using Base.Caching.Pipelines;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseThisMonthQuery.Dtos
{
	public class Get_TotalExpenseThisMonth_QueryDto : IRequest<Get_TotalExpenseThisMonth_ResponseDto>, ICachableRequest
	{
		public int MonthStartDay { get; set; }

		public bool IsBypassCache { get; set; }
		public string CacheKeyParameters => MonthStartDay.ToString();
	}
}