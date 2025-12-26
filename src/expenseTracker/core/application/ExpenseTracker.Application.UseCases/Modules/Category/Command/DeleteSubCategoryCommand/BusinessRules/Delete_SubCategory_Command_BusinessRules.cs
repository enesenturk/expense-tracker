
using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand.BusinessRules
{
	public class Delete_SubCategory_Command_BusinessRules
	{
		internal void EnsureIsNotOther(t_sub_category record)
		{
			if (record.is_other)
				throw new BusinessRuleException($"{record.name} {uiMessage.Other_category_can_not_be_deleted}");
		}
	}
}