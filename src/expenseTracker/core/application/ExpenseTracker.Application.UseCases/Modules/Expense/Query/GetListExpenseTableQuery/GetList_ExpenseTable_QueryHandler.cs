using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery
{
	public class GetList_ExpenseTable_QueryHandler : UseCaseHandler<GetList_ExpenseTable_QueryDto, GetList_ExpenseTable_ResponseDto>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;

		public GetList_ExpenseTable_QueryHandler(IMapper mapper,
			IExpenseRepository expenseRepository
			) : base(mapper)
		{
			_expenseRepository = expenseRepository;
		}

		#endregion

		public override async Task<GetList_ExpenseTable_ResponseDto> Handle(GetList_ExpenseTable_QueryDto query, CancellationToken cancellationToken)
		{
			List<t_expense> records = await _expenseRepository.GetListAsync(
				orderBy: o => o.OrderByDescending(i => i.date).ThenBy(n => n.t_sub_category_id)
				);

			return new GetList_ExpenseTable_ResponseDto
			{
				Records = _mapper.Map<List<GetList_ExpenseTable_SingleResponseDto>>(records)
			};
		}

	}
}
