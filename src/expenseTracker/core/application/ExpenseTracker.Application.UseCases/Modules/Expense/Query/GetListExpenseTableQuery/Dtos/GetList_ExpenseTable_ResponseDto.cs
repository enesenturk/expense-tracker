namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery.Dtos
{
	public class GetList_ExpenseTable_ResponseDto
	{
		public List<GetList_ExpenseTable_SingleResponseDto> Records { get; set; }
	}

	public class GetList_ExpenseTable_SingleResponseDto
	{

		public Guid CategoryId { get; set; }
		public Guid SubCategoryId { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public bool IsNecessary { get; set; }
		public Guid Id { get; set; }

	}
}
