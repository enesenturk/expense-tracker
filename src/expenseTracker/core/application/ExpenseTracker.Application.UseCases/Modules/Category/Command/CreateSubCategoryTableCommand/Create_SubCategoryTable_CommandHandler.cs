using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand
{
	public class Create_SubCategoryTable_CommandHandler : UseCaseHandler<Create_SubCategoryTable_CommandDto, Unit>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		private readonly Create_SubCategoryTable_Command_BusinessRules _businessRules;

		public Create_SubCategoryTable_CommandHandler(IMapper mapper,

			ISubCategoryRepository subCategoryRepository,

			Create_SubCategoryTable_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Create_SubCategoryTable_CommandDto command, CancellationToken cancellationToken)
		{
			_businessRules.EnsureIsUnique(command.Records);

			List<t_sub_category> currentRecords = await _subCategoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<t_sub_category> newRecords = _mapper.Map<List<t_sub_category>>(command.Records);

			if (command.IsClearExistingData)
			{
				await _subCategoryRepository.DeleteRangeAsync(currentRecords);

				await _subCategoryRepository.AddRangeAsync(newRecords);
			}
			else
			{
				List<t_sub_category> recordsToAdd = new List<t_sub_category>();

				foreach (var categoryGroup in newRecords.GroupBy(c => c.t_category_id))
				{
					foreach (var record in categoryGroup)
					{
						bool isIdAlreadyExists = categoryGroup.FirstOrDefault(x => x.id == record.id) != null;

						if (isIdAlreadyExists)
							continue;

						record.name = ListHelper.GetUniqueName(categoryGroup.Select(n => n.name).ToList(), record.name);

						recordsToAdd.Add(record);
					}
				}

				if (recordsToAdd.Any())
					await _subCategoryRepository.AddRangeAsync(recordsToAdd);
			}

			return Unit.Value;
		}

	}
}
