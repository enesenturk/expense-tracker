using ExpenseTracker.MobileApp.Base.Models;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Response
{
	public class GetList_SubCategory_SingleResponseModel : ListRecordModel
	{
		public string Name { get; set; }
		public bool IsOther { get; set; }
	}
}
