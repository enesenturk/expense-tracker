using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.Dtos;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.BusinessRules
{
	public class Create_CategoryTable_Command_BusinessRules
	{
		internal void EnsureIsUnique(List<Create_CategoryTable_SingleCommandDto> records)
		{
			bool isNotUnique = records.GroupBy(n => n.Name).FirstOrDefault(g => g.Count() > 1) != null ||
				records.GroupBy(n => n.Id).FirstOrDefault(g => g.Count() > 1) != null;

			if (isNotUnique)
				throw new BusinessRuleException(uiMessage.Csv_contains_duplicated_elements);
		}
	}
}
