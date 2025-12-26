using Base.Dto;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos
{
	public class GetList_Expense_ResponseDto
	{
		public List<GetList_Expense_SingleResponseDto> Records { get; set; }
	}

	public class GetList_Expense_SingleResponseDto : ListRecordDto
	{
		public Guid SubCategoryId { get; set; }
		public string SubCategoryName { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public bool IsNecessary { get; set; }
	}
}