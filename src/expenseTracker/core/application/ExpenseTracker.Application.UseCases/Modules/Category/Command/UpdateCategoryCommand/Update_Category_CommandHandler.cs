using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand
{
	public class Update_Category_CommandHandler : UseCaseHandler<Update_Category_CommandDto, Unit>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		private readonly Update_Category_Command_BusinessRules _businessRules;

		public Update_Category_CommandHandler(IMapper mapper,

			ICategoryRepository categoryRepository,

			Update_Category_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_categoryRepository = categoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Update_Category_CommandDto command, CancellationToken cancellationToken)
		{
			await _businessRules.EnsureNameIsUnique(command.Id, command.Name);

			t_category record = await _categoryRepository.GetAsync(command.Id);

			record.name = command.Name;

			await _categoryRepository.UpdateAsync(record);

			return Unit.Value;
		}

	}
}
