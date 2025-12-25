using Base.Dto.CRUD;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand.Dtos
{
	public class Update_SubCategory_CommandDto : Get_Request, IRequest<Unit>
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
	}
}
