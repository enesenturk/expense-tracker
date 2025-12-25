using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand
{
	public class Update_SubCategory_CommandHandler : UseCaseHandler<Update_SubCategory_CommandDto, Unit>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		private readonly Update_SubCategory_Command_BusinessRules _businessRules;

		public Update_SubCategory_CommandHandler(IMapper mapper,

			ISubCategoryRepository subCategoryRepository,

			Update_SubCategory_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Update_SubCategory_CommandDto command, CancellationToken cancellationToken)
		{
			await _businessRules.EnsureNameIsUnique(command.Id, command.CategoryId, command.Name);

			t_sub_category record = await _subCategoryRepository.GetAsync(command.Id);

			record.name = command.Name;

			await _subCategoryRepository.UpdateAsync(record);

			return Unit.Value;
		}

	}
}
