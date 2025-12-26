using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos
{
	public class GetList_Expense_QueryDto : IRequest<GetList_Expense_ResponseDto>
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}
