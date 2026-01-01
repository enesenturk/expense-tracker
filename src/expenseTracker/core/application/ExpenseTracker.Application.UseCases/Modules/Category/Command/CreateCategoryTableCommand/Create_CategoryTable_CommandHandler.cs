using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand
{
	public class Create_CategoryTable_CommandHandler : UseCaseHandler<Create_CategoryTable_CommandDto, Unit>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		private readonly Create_CategoryTable_Command_BusinessRules _businessRules;

		public Create_CategoryTable_CommandHandler(IMapper mapper,

			ICategoryRepository categoryRepository,

			Create_CategoryTable_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_categoryRepository = categoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Create_CategoryTable_CommandDto command, CancellationToken cancellationToken)
		{
			_businessRules.EnsureIsUnique(command.Records);

			List<t_category> currentRecords = await _categoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<t_category> newRecords = _mapper.Map<List<t_category>>(command.Records);

			if (command.IsClearExistingData)
			{
				await _categoryRepository.DeleteRangeAsync(currentRecords);

				await _categoryRepository.AddRangeAsync(newRecords);
			}
			else
			{
				List<t_category> recordsToAdd = new List<t_category>();

				foreach (var record in newRecords)
				{
					bool isIdAlreadyExists = currentRecords.FirstOrDefault(x => x.id == record.id) != null;

					if (isIdAlreadyExists)
						continue;

					record.name = ListHelper.GetUniqueName(currentRecords.Select(n => n.name).ToList(), record.name);

					recordsToAdd.Add(record);
				}

				if (recordsToAdd.Any())
					await _categoryRepository.AddRangeAsync(recordsToAdd);
			}

			return Unit.Value;
		}

	}
}
