
using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.BusinessRules
{
	public class Create_SubCategory_Command_BusinessRules
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		public Create_SubCategory_Command_BusinessRules(ISubCategoryRepository subCategoryRepository)
		{
			_subCategoryRepository = subCategoryRepository;
		}

		#endregion

		internal async Task EnsureNameIsUnique(Guid categoryId, string name)
		{
			List<t_sub_category> records = await _subCategoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(g => g.id),
				predicate: x => x.t_category_id == categoryId && x.name == name
				);

			if (records.Count > 0)
				throw new BusinessRuleException(uiMessage.Category_already_exists);
		}
	}
}
