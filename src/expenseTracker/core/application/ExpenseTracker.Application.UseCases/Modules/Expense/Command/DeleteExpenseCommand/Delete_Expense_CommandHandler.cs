using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.DeleteExpenseCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.DeleteExpenseCommand
{
	public class Delete_Expense_CommandHandler : UseCaseHandler<Delete_Expense_CommandDto, Unit>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;

		public Delete_Expense_CommandHandler(IMapper mapper,

			IExpenseRepository expenseRepository
			) : base(mapper)
		{
			_expenseRepository = expenseRepository;
		}

		#endregion

		public override async Task<Unit> Handle(Delete_Expense_CommandDto command, CancellationToken cancellationToken)
		{
			Guid expenseId = command.Id;

			t_expense record = await _expenseRepository.GetAsync(expenseId);

			await _expenseRepository.DeleteAsync(record);

			return Unit.Value;
		}

	}
}
