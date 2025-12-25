using Base.Dto.CRUD;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.Dtos
{
	public class Update_Category_CommandDto : Get_Request, IRequest<Unit>
	{
		public string Name { get; set; }
	}
}