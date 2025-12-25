using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteCategoryCommand.Dtos
{
	public class Delete_Category_CommandDto : IRequest<Delete_Category_ResponseDto>
	{
		public Guid Id { get; set; }
		public bool IsApproved { get; set; }
	}
}