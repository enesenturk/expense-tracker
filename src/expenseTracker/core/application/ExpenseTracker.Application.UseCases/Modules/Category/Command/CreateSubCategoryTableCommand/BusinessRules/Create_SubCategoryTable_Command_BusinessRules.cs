using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.Dtos;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.BusinessRules
{
	public class Create_SubCategoryTable_Command_BusinessRules
	{
		internal void EnsureIsUnique(List<Create_SubCategoryTable_SingleCommandDto> records)
		{
			bool isNotUnique = records.GroupBy(n => new { n.Name, n.CategoryId }).FirstOrDefault(g => g.Count() > 1) != null ||
				records.GroupBy(n => n.Id).FirstOrDefault(g => g.Count() > 1) != null;

			if (isNotUnique)
				throw new BusinessRuleException(uiMessage.Csv_contains_duplicated_elements);
		}
	}
}
