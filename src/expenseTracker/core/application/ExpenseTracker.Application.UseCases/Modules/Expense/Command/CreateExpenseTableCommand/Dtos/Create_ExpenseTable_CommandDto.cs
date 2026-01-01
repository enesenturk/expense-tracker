using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand.Dtos
{
	public class Create_ExpenseTable_CommandDto : IRequest<Unit>
	{
		public bool IsClearExistingData { get; set; }
		public List<Create_ExpenseTable_SingleCommandDto> Records { get; set; }
	}

	public class Create_ExpenseTable_SingleCommandDto
	{
		public Guid CategoryId { get; set; }
		public Guid SubCategoryId { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public bool IsNecessary { get; set; }
		public Guid Id { get; set; }
	}
}
