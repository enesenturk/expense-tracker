using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.DeleteExpenseCommand.Dtos
{
	public class Delete_Expense_CommandDto : IRequest<Unit>
	{
		public Guid Id { get; set; }
	}
}
