using Base.DataAccess.Entity;

namespace ExpenseTracker.Domain.Entities
{
	public class t_expense : IEntity
	{

		public Guid t_category_id { get; set; }
	}
}
