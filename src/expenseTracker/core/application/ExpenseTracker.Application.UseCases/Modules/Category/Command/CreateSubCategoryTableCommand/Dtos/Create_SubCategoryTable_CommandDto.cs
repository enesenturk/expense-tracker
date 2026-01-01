using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.Dtos
{
	public class Create_SubCategoryTable_CommandDto : IRequest<Unit>
	{
		public bool IsClearExistingData { get; set; }
		public List<Create_SubCategoryTable_SingleCommandDto> Records { get; set; }
	}

	public class Create_SubCategoryTable_SingleCommandDto
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
