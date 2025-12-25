using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos
{
	public class GetList_Category_QueryDto : IRequest<GetList_Category_ResponseDto>
	{
		public string Culture { get; set; }
	}
}
