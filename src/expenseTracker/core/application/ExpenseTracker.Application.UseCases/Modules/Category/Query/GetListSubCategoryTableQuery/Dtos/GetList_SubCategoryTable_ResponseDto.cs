namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Dtos
{
	public class GetList_SubCategoryTable_ResponseDto
	{
		public List<GetList_SubCategoryTable_SingleResponseDto> Records { get; set; }
	}

	public class GetList_SubCategoryTable_SingleResponseDto
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
