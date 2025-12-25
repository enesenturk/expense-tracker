using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos
{
	public class Create_Category_CommandDto : IRequest<Unit>
	{
		public string Name { get; set; }
		public string Culture { get; set; }
	}
}