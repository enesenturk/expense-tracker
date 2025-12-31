using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery.Dtos
{
	public class GetList_ExpenseTable_QueryDto : IRequest<GetList_ExpenseTable_ResponseDto>
	{
	}
}
