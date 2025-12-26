using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery
{
	public class GetList_Expense_QueryHandler : UseCaseHandler<GetList_Expense_QueryDto, GetList_Expense_ResponseDto>
	{

		#region CTOR

		private readonly IExpenseRepository _expenseRepository;
		private readonly ISubCategoryRepository _subCategoryRepository;

		public GetList_Expense_QueryHandler(IMapper mapper, IExpenseRepository expenseRepository, ISubCategoryRepository subCategoryRepository) : base(mapper)
		{
			_expenseRepository = expenseRepository;
			_subCategoryRepository = subCategoryRepository;
		}

		#endregion

		public override async Task<GetList_Expense_ResponseDto> Handle(GetList_Expense_QueryDto query, CancellationToken cancellationToken)
		{
			//Expression<Func<t_expense, bool>> predicate = x => x.culture == query.Culture;

			List<t_expense> records = await _expenseRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.date).ThenBy(n => n.t_sub_category_id)
				);

			List<t_sub_category> subCategories = await _subCategoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(n => n.name)
				);

			List<GetList_Expense_SingleResponseDto> mappedRecords = ListHelper.MapListRecord<t_expense, GetList_Expense_SingleResponseDto>(_mapper, records);

			mappedRecords.ForEach(mr =>
			{
				mr.SubCategoryName = subCategories.FirstOrDefault(sc => sc.id == mr.SubCategoryId)?.name;
			});

			return new GetList_Expense_ResponseDto
			{
				Records = mappedRecords
			};
		}

	}
}
