namespace ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Request
{
	public class Update_SubCategory_RequestModel
	{
		public Guid Id { get; set; }
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
	}
}
