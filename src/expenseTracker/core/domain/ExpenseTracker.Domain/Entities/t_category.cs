using Base.DataAccess.Entity;

namespace ExpenseTracker.Domain.Entities
{
	public class t_category : IEntity
	{
		public string name { get; set; }
		public string culture { get; set; }
		public bool is_expense_created { get; set; }
	}
}