using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand
{
	public class Create_SubCategory_CommandHandler : UseCaseHandler<Create_SubCategory_CommandDto, Create_SubCategory_ResponseDto>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		private readonly Create_SubCategory_Command_BusinessRules _businessRules;

		public Create_SubCategory_CommandHandler(IMapper mapper,

			ISubCategoryRepository subCategoryRepository,

			Create_SubCategory_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Create_SubCategory_ResponseDto> Handle(Create_SubCategory_CommandDto command, CancellationToken cancellationToken)
		{
			await _businessRules.EnsureNameIsUnique(command.CategoryId, command.Name);

			t_sub_category created = await _subCategoryRepository.AddAsync(new t_sub_category
			{
				name = command.Name,
				t_category_id = command.CategoryId,
				is_expense_created = false,
			});

			return new Create_SubCategory_ResponseDto
			{
				Id = created.id
			};
		}

	}
}
