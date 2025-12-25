using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand
{
	public class Create_Category_CommandHandler : UseCaseHandler<Create_Category_CommandDto, Unit>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		private readonly Create_Category_Command_BusinessRules _businessRules;

		public Create_Category_CommandHandler(IMapper mapper,

			ICategoryRepository categoryRepository,

			Create_Category_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_categoryRepository = categoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Create_Category_CommandDto command, CancellationToken cancellationToken)
		{
			await _businessRules.EnsureNameIsUnique(command.Name);

			await _categoryRepository.AddAsync(new t_category
			{
				culture = command.Culture,
				name = command.Name,
				is_expense_created = false
			});

			return Unit.Value;
		}

	}
}
