using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand
{
	public class Create_ExpenseTable_CommandHandler : UseCaseHandler<Create_ExpenseTable_CommandDto, Unit>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;

		private readonly Create_ExpenseTable_Command_BusinessRules _businessRules;

		public Create_ExpenseTable_CommandHandler(IMapper mapper,

			IExpenseRepository expenseRepository,

			Create_ExpenseTable_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_expenseRepository = expenseRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Unit> Handle(Create_ExpenseTable_CommandDto command, CancellationToken cancellationToken)
		{
			_businessRules.EnsureIsUnique(command.Records);

			List<t_expense> currentRecords = await _expenseRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<t_expense> newRecords = _mapper.Map<List<t_expense>>(command.Records);

			if (command.IsClearExistingData)
			{
				await _expenseRepository.DeleteRangeAsync(currentRecords);

				await _expenseRepository.AddRangeAsync(newRecords);
			}
			else
			{
				List<t_expense> recordsToAdd = new List<t_expense>();

				foreach (var record in newRecords)
				{
					bool isIdAlreadyExists = currentRecords.FirstOrDefault(x => x.id == record.id) != null;

					if (isIdAlreadyExists)
						continue;

					recordsToAdd.Add(record);
				}

				if (recordsToAdd.Any())
					await _expenseRepository.AddRangeAsync(recordsToAdd);
			}

			return Unit.Value;
		}

	}
}
