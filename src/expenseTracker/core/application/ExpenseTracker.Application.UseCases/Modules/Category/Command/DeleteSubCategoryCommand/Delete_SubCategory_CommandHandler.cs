using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand.BusinessRules;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Expense;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand
{
	public class Delete_SubCategory_CommandHandler : UseCaseHandler<Delete_SubCategory_CommandDto, Delete_SubCategory_ResponseDto>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;
		private readonly IExpenseRepository _expenseRepository;

		private readonly Delete_SubCategory_Command_BusinessRules _businessRules;

		public Delete_SubCategory_CommandHandler(IMapper mapper,

			ISubCategoryRepository subCategoryRepository,
			IExpenseRepository expenseRepository,

			Delete_SubCategory_Command_BusinessRules businessRules
			) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;
			_expenseRepository = expenseRepository;

			_businessRules = businessRules;
		}

		#endregion

		public override async Task<Delete_SubCategory_ResponseDto> Handle(Delete_SubCategory_CommandDto command, CancellationToken cancellationToken)
		{
			Guid categoryId = command.Id;

			t_sub_category record = await _subCategoryRepository.GetAsync(categoryId);

			_businessRules.EnsureIsNotOther(record);

			if (record.is_expense_created && !command.IsApproved)
			{
				return new Delete_SubCategory_ResponseDto
				{
					IsApprovalRequired = true
				};
			}
			else if (record.is_expense_created && command.IsApproved)
			{
				List<t_expense> expenses = await _expenseRepository.GetListAsync(
					orderBy: o => o.OrderBy(g => g.id),
					predicate: x => x.t_sub_category_id == categoryId
					);

				await _expenseRepository.DeleteRangeAsync(expenses);

				await _subCategoryRepository.DeleteAsync(record);

				return new Delete_SubCategory_ResponseDto
				{
					IsApprovalRequired = false
				};
			}
			else
			{
				await _subCategoryRepository.DeleteAsync(record);

				return new Delete_SubCategory_ResponseDto
				{
					IsApprovalRequired = false
				};
			}
		}

	}
}
