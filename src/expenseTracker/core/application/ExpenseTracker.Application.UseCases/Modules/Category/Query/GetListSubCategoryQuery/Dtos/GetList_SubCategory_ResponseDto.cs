using Base.Dto;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos
{
	public class GetList_SubCategory_ResponseDto
	{
		public List<GetList_SubCategory_SingleResponseDto> Records { get; set; }
	}

	public class GetList_SubCategory_SingleResponseDto : ListRecordDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
