using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;
using MediatR;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand
{
	public class Create_Expense_CommandHandler : UseCaseHandler<Create_Expense_CommandDto, Unit>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;

		public Create_Expense_CommandHandler(IMapper mapper,

			IExpenseRepository expenseRepository
			) : base(mapper)
		{
			_expenseRepository = expenseRepository;
		}

		#endregion

		public override async Task<Unit> Handle(Create_Expense_CommandDto command, CancellationToken cancellationToken)
		{
			t_expense record = _mapper.Map<t_expense>(command);

			await _expenseRepository.AddAsync(record);

			return Unit.Value;
		}

	}
}