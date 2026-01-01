using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.Dtos
{
	public class Create_CategoryTable_CommandDto : IRequest<Unit>
	{
		public bool IsClearExistingData { get; set; }
		public List<Create_CategoryTable_SingleCommandDto> Records { get; set; }
	}

	public class Create_CategoryTable_SingleCommandDto
	{
		public string Name { get; set; }
		public string Culture { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
