using Base.DataAccess.Entity;

namespace ExpenseTracker.Domain.Entities
{
	public class t_expense : IEntity
	{

		public Guid t_category_id { get; set; }
		public Guid t_sub_category_id { get; set; }
		public DateTime date { get; set; }
		public decimal amount { get; set; }
		public bool is_necessary { get; set; }

	}
}