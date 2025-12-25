using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.Dtos
{
	public class Create_SubCategory_CommandDto : IRequest<Unit>
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
	}
}