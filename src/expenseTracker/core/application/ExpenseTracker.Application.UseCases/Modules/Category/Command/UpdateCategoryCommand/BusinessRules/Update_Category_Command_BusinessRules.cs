using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.BusinessRules
{
	public class Update_Category_Command_BusinessRules
	{

		#region CTOR

		private readonly ICategoryRepository _categoryRepository;

		public Update_Category_Command_BusinessRules(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		#endregion

		internal async Task EnsureNameIsUnique(Guid id, string name)
		{
			List<t_category> records = await _categoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(g => g.id),
				predicate: x => x.id != id && x.name == name
				);

			if (records.Count > 0)
				throw new BusinessRuleException(uiMessage.Category_already_exists);
		}
	}
}
