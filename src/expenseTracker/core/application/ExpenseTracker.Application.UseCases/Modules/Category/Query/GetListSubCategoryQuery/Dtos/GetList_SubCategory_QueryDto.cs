using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos
{
	public class GetList_SubCategory_QueryDto : IRequest<GetList_SubCategory_ResponseDto>
	{
		public Guid CategoryId { get; set; }
	}
}
