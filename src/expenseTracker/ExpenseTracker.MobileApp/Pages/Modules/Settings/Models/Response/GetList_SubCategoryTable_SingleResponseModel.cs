namespace ExpenseTracker.MobileApp.Pages.Modules.Settings.Models.Response
{
	public class GetList_SubCategoryTable_SingleResponseModel
	{
		public Guid CategoryId { get; set; }
		public string Name { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
