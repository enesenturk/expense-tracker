using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand.Dtos;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseTableCommand.BusinessRules
{
	public class Create_ExpenseTable_Command_BusinessRules
	{
		internal void EnsureIsUnique(List<Create_ExpenseTable_SingleCommandDto> records)
		{
			bool isNotUnique = records.GroupBy(n => n.Id).FirstOrDefault(g => g.Count() > 1) != null;

			if (isNotUnique)
				throw new BusinessRuleException(uiMessage.Csv_contains_duplicated_elements);
		}
	}
}
