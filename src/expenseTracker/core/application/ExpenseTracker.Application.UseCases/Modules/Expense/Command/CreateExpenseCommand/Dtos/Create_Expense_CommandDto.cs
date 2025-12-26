using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos
{
	public class Create_Expense_CommandDto : IRequest<Unit>
	{

		public Guid CategoryId { get; set; }
		public Guid SubCategoryId { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public bool IsNecessary { get; set; }

	}
}