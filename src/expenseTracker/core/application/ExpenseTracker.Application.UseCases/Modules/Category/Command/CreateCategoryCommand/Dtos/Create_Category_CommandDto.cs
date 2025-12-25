using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos
{
	public class Create_Category_CommandDto : IRequest<Create_Category_ResponseDto>
	{
		public string Name { get; set; }
		public string Culture { get; set; }
	}
}