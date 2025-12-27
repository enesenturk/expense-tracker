using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseThisMonthQuery.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseThisMonthQuery
{
	public class Get_TotalExpenseThisMonth_QueryHandler : UseCaseHandler<Get_TotalExpenseThisMonth_QueryDto, Get_TotalExpenseThisMonth_ResponseDto>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;

		public Get_TotalExpenseThisMonth_QueryHandler(IMapper mapper,
			IExpenseRepository expenseRepository
			) : base(mapper)
		{
			_expenseRepository = expenseRepository;
		}

		#endregion

		public override async Task<Get_TotalExpenseThisMonth_ResponseDto> Handle(Get_TotalExpenseThisMonth_QueryDto query, CancellationToken cancellationToken)
		{
			DateTime now = DateTime.Today;

			DateTime monthStart = new DateTime(now.Year, now.Month, 1);
			DateTime nextMonthStart = monthStart.AddMonths(1);

			Expression<Func<t_expense, bool>> predicate = x => x.date >= monthStart && x.date < nextMonthStart;

			List<t_expense> records = await _expenseRepository.GetListAsync(
				orderBy: o => o.OrderByDescending(i => i.date).ThenBy(n => n.t_sub_category_id),
				predicate: predicate
				);

			decimal totalAmount = records.Sum(a => a.amount);

			return new Get_TotalExpenseThisMonth_ResponseDto
			{
				TotalAmount = totalAmount
			};
		}

	}
}