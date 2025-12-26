using Base.DataAccess.Entity;

namespace ExpenseTracker.Domain.Entities
{
	public class t_sub_category : IEntity
	{
		public Guid t_category_id { get; set; }
		public string name { get; set; }
		public bool is_expense_created { get; set; }
		public bool is_other { get; set; }
	}
}