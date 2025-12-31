namespace ExpenseTracker.MobileApp.Pages.Modules.Settings.Models.Response
{
	public class GetList_CategoryTable_SingleResponseModel
	{
		public string Name { get; set; }
		public string Culture { get; set; }
		public bool IsExpenseCreated { get; set; }
		public bool IsOther { get; set; }
		public Guid Id { get; set; }
	}
}
