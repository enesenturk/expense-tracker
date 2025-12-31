namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos
{
	public class GetList_CategoryTable_ResponseDto
	{
		public List<GetList_CategoryTable_SingleResponseDto> Records { get; set; }
	}

	public class GetList_CategoryTable_SingleResponseDto
	{
		public string Name { get; set; }
		public string Culture { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
