using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteCategoryCommand
{
	public class Delete_Category_CommandHandler : UseCaseHandler<Delete_Category_CommandDto, Delete_Category_ResponseDto>
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;
		private readonly IExpenseRepository _expenseRepository;

		public Delete_Category_CommandHandler(IMapper mapper,

			ICategoryRepository categoryRepository,
			IExpenseRepository expenseRepository
			) : base(mapper)
		{
			_categoryRepository = categoryRepository;
			_expenseRepository = expenseRepository;
		}

		#endregion

		public override async Task<Delete_Category_ResponseDto> Handle(Delete_Category_CommandDto command, CancellationToken cancellationToken)
		{
			Guid categoryId = command.Id;

			t_category record = await _categoryRepository.GetAsync(categoryId);

			if (record.is_expense_created && !command.IsApproved)
			{
				return new Delete_Category_ResponseDto
				{
					IsApprovalRequired = true
				};
			}
			else if (record.is_expense_created && command.IsApproved)
			{
				List<t_expense> expenses = await _expenseRepository.GetListAsync(
					orderBy: o => o.OrderBy(g => g.id),
					predicate: x => x.t_category_id == categoryId
					);

				await _expenseRepository.DeleteRangeAsync(expenses);

				await _categoryRepository.DeleteAsync(record);

				return new Delete_Category_ResponseDto
				{
					IsApprovalRequired = false
				};
			}
			else
			{
				await _categoryRepository.DeleteAsync(record);

				return new Delete_Category_ResponseDto
				{
					IsApprovalRequired = false
				};
			}
		}

	}
}
