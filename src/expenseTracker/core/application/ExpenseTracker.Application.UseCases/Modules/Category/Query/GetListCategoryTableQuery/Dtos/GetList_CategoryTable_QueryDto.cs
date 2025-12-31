using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos
{
	public class GetList_CategoryTable_QueryDto : IRequest<GetList_CategoryTable_ResponseDto>
	{
		public string Culture { get; set; }
	}
}
