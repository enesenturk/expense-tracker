namespace ExpenseTracker.MobileApp.Pages.Modules.Settings.Models.Response
{
	public class GetList_ExpenseTable_SingleResponseModel
	{
		public Guid CategoryId { get; set; }
		public Guid SubCategoryId { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public bool IsNecessary { get; set; }
		public Guid Id { get; set; }
	}
}
