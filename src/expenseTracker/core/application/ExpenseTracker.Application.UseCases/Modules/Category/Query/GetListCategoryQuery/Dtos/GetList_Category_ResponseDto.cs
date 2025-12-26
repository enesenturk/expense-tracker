using Base.Dto;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos
{
	public class GetList_Category_ResponseDto
	{
		public List<GetList_Category_SingleResponseDto> Records { get; set; }
	}

	public class GetList_Category_SingleResponseDto : ListRecordDto
	{
		public string Name { get; set; }
		public bool IsOther { get; set; }
	}
}
