using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand.Dtos
{
	public class Delete_SubCategory_CommandDto : IRequest<Delete_SubCategory_ResponseDto>
	{
		public Guid Id { get; set; }
		public bool IsApproved { get; set; }
	}
}
