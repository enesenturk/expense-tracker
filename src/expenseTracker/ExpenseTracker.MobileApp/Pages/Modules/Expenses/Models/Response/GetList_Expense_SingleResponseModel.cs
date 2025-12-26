using ExpenseTracker.MobileApp.Base.Models;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses.Models.Response
{
	public class GetList_Expense_SingleResponseModel : ListRecordModel
	{
		public DateTime Date { get; set; }
		public string Date_formatted { get; set; }
		public string SubCategoryName { get; set; }
		public decimal Amount { get; set; }
		public string Necessary { get; set; }
	}
}